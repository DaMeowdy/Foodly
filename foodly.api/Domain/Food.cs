using System;
using System.Collections.Generic;
using System.Text.Json;

namespace foodly.api.Domain;

public partial class Food
{
    public Guid FoodId { get; set; }

    public string? FoodSlug { get; set; }

    public string? FoodName { get; set; }

    public string? FoodImageUrl { get; set; }

    public int Cost { get; set; }

    public JsonDocument? FoodDietaryRequirements { get; set; }
}
