using RectanglesSearch.Api.Models;

namespace RectanglesSearch.Api.Repositories
{
    public interface IRectangleRepository
    {
        Task<Dictionary<string, List<Rectangle>>> RectanglesSearchByCoordinates(List<RectangleSearchRequest> coordinates);
    }
}
