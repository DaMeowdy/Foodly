using foodly.api.DTO;
using foodly.api.features.food;
using MediatR;

public static class FoodEndpoints
{
    public static void MapFoodEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/foods", async (ISender sender, string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize
        ) =>
        {
            GetAllFoodCommand getAllFoodCommand = new GetAllFoodCommand(searchTerm, sortColumn, sortOrder, page, pageSize);
            PagedList<FoodDTO> response = await sender.Send(getAllFoodCommand, new CancellationToken());
            return response;
        });
    }
}