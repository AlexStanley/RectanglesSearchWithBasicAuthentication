using RectanglesSearch.Api.Models;

namespace RectanglesSearch.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByNameAsync(string name);
    }
}
