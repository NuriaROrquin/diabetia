using Diabetia.Domain.Entities;
using Diabetia.Infrastructure.DTO;
using Diabetia.Infrastructure.Providers;

namespace Diabetia.Domain.Services;
using Refit;

public interface IApiService
{
    [Multipart]
    [Post("/v2/image/segmentation/complete/v1.0?language=spa")]
    Task<FoodDish> DetectFoodDish([Header("Authorization")] string authorization, [AliasAs("image")] StreamPart image);

    [Post("/v2/nutrition/recipe/nutritionalInfo/v1.0?language=spa")]
    Task<NutritionalInfoDTO> GetNutrientsPerIngredients([Header("Authorization")] string authorization, [Body] ImageRequest imageRequest);
}