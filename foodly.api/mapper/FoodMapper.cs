using foodly.api.Domain;
using foodly.api.DTO;

public static class FoodMapper
{
    public static FoodDTO ToDTO(this Food food) => new(food.FoodSlug, food.FoodName, food.FoodImageUrl, food.Cost, food.FoodDietaryRequirements);

}