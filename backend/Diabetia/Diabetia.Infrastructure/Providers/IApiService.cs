using Diabetia.Domain.Entities;
namespace Diabetia.Domain.Services;
using Refit;

public interface IApiService
{
    [Multipart]
    [Post("/v2/image/segmentation/complete/v1.0?language=spa")]
    Task<FoodDish> DetectFoodDish([Header("Authorization")] string authorization, [AliasAs("image")] StreamPart image);

    [Multipart]
    [Post("/v2/nutrition/recipe/nutritionalInfo/v1.0?language=spa")]
    Task<IngredientsDetected> GetNutrientsPerIngredients([Header("Authorization")] string authorization, [AliasAs("json")] FoodDish foodDish);
}