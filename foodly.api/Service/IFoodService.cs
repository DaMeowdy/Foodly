using foodly.api.Domain;
using foodly.api.DTO;

namespace foodly.api.Services;

public interface IFoodService
{
    public Task<PagedList<FoodDTO>> GetFoodsAsync(string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize);
}