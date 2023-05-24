using RectanglesSearch.Api.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace RectanglesSearch.Api.Middlewares
{
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int SaltByteSize = 24;
        private readonly int HashByteSize = 24;
        private readonly int HasingIterationsCount = 10101;

        public BasicAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task? Invoke(HttpContext httpContext)
        {
            string authHeader = httpContext.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Basic"))
            {
                string ecodeUsernameAndPassword = authHeader["Basic ".Length..].Trim();
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                string usernameAndPassword =
                    encoding.GetString(Convert.FromBase64String(ecodeUsernameAndPassword));
                int index = usernameAndPassword.IndexOf(':');
                string userName = usernameAndPassword.Substring(0, index);
                string password = usernameAndPassword[(index + 1)..];

                var userRepository = httpContext.RequestServices.GetRequiredService<UserRepository>();
                var user = await userRepository.GetUserByNameAsync(userName);

                if (string.IsNullOrEmpty(user?.UserName))
                {
                    httpContext.Response.StatusCode = 401;
                    return;
                }
                else
                {
                    if (VerifyHashedPassword(user.Password, password))
                    {
                        await _next.Invoke(httpContext);
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 401;
                        return;
                    }
                }
            }
            else
            {
                httpContext.Response.StatusCode = 401;
                return;
            }
        }

        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] _passwordHashBytes;

            int _arrayLen = (SaltByteSize + HashByteSize) + 1;

            if (hashedPassword == null)
                return false;

            if (password == null)
                throw new ArgumentNullException(nameof(password));

            byte[] src = Convert.FromBase64String(hashedPassword);

            if ((src.Length != _arrayLen) || (src[0] != 0))
                return false;

            byte[] _currentSaltBytes = new byte[SaltByteSize];
            Buffer.BlockCopy(src, 1, _currentSaltBytes, 0, SaltByteSize);

            byte[] _currentHashBytes = new byte[HashByteSize];
            Buffer.BlockCopy(src, SaltByteSize + 1, _currentHashBytes, 0, HashByteSize);

            using (Rfc2898DeriveBytes bytes = new(password, _currentSaltBytes, HasingIterationsCount))
                _passwordHashBytes = bytes.GetBytes(SaltByteSize);

            return AreHashesEqual(_currentHashBytes, _passwordHashBytes);

        }

        private bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;

            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];

            return 0 == xor;
        }
    }

    public static class BasicAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseBasicAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthenticationMiddleware>();
        }
    }
}
