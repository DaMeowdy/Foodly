using MediatR;
using foodly.api.DTO;
using foodly.api.Services;

namespace foodly.api.features.food;

public record GetAllFoodCommand(string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize) : IRequest<PagedList<FoodDTO>>;

public class GetAllFoodHandler : IRequestHandler<GetAllFoodCommand, PagedList<FoodDTO>>
{
    private IFoodService _foodService;
    public GetAllFoodHandler(IFoodService foodService)
    {
        this._foodService = foodService;
    }
    public async Task<PagedList<FoodDTO>> Handle(GetAllFoodCommand request, CancellationToken cancellationToken)
    {
        var foods = await _foodService.GetFoodsAsync(request.searchTerm, request.sortColumn, request.sortOrder, request.page, request.pageSize);
        return foods;
    }
}