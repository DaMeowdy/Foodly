using System.Linq.Expressions;
using foodly.api.Domain;
using foodly.api.DTO;
using foodly.api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace foodly.api.Services;

public class FoodService : IFoodService
{

    private readonly FoodlyContext _context;
    public FoodService(FoodlyContext context)
    {
        this._context = context;
    }
    public async Task<PagedList<FoodDTO>> GetFoodsAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize)
    {
        IQueryable<Food> FoodsQuery = _context.Foods;

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            FoodsQuery.Where(f => f.FoodName.Contains(searchTerm) || f.FoodSlug.Contains(searchTerm));
        }

        Expression<Func<Food, Object>> keySelector = sortColumn switch
        {
            "price" => Food => Food.Cost,
            _ => Food => Food.FoodName
        };
        if (sortOrder is null)
            FoodsQuery = FoodsQuery.OrderBy(keySelector);
        else if (sortOrder.ToLower() == "desc")
            FoodsQuery = FoodsQuery.OrderByDescending(keySelector);
        else
            FoodsQuery = FoodsQuery.OrderBy(keySelector);


        IQueryable<FoodDTO> foodDTOs = FoodsQuery.Select(f => f.ToDTO());

        var FoodList = await PagedList<FoodDTO>.CreateAsync(foodDTOs, page, pageSize);

        return FoodList;
    }

}