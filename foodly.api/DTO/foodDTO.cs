using System.Text.Json;
using foodly.api.Domain;

namespace foodly.api.DTO;

public class FoodDTO
{
    public string FoodSlug { get; set; }

    public string FoodName { get; set; }

    public string FoodImageUrl { get; set; }

    public int Cost { get; set; }

    public JsonDocument FoodDietaryRequirements { get; set; }
    public FoodDTO(string slug, string name, string url, int cost, JsonDocument fdr)
    {
        FoodSlug = slug;
        FoodName = name;
        FoodImageUrl = url;
        Cost = cost;
        FoodDietaryRequirements = fdr;
    }



}