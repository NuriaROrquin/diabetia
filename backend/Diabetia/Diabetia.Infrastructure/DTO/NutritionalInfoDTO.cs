using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace Diabetia.Infrastructure.DTO;

public class NutritionalInfoDTO
{
    [JsonPropertyName("imageId")]
    public int ImageId { get; set; }

    [JsonPropertyName("nutritional_info_per_item")]
    public List<NutritionalInfoPerItemObject> NutritionalInfoPerItem { get; set; }

    [JsonPropertyName("serving_size")]
    public float ServingSize { get; set; }
}

public class NutritionalInfoObject
{
    [JsonPropertyName("totalNutrients")]
    public Dictionary<string, NutrientCodeSample> TotalNutrients { get; set; }
}

public class NutrientCodeSample
{
    [JsonPropertyName("label")]
    public string Label { get; set; }

    [JsonPropertyName("quantity")]
    public float Quantity { get; set; }
}

public class NutritionalInfoPerItemObject
{
    [JsonPropertyName("serving_size")]
    public float ServingSize { get; set; }

    [JsonPropertyName("nutritional_info")]
    public NutritionalInfoObject NutritionalInfo { get; set; }
    
    [JsonPropertyName("food_item_position")]
    public int FoodItemPosition { get; set; }
}