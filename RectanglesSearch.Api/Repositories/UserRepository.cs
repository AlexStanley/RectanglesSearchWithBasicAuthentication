using Microsoft.EntityFrameworkCore;
using RectanglesSearch.Api.Models;

namespace RectanglesSearch.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RectangleDbContext _context;

        public UserRepository(RectangleDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByNameAsync(string name)
        {
            return await _context.Users.Where(x => x.UserName.Equals(name)).FirstOrDefaultAsync();
        }
    }
}
