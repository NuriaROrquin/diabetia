using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using System.Text.RegularExpressions;


namespace Diabetia.Application.UseCases
{
    public class TagDetectionUseCase
    {
        private readonly ITagRecognitionProvider _apiAmazonService;

        public TagDetectionUseCase(ITagRecognitionProvider apiAmazonService)
        {
            _apiAmazonService = apiAmazonService;
        }

        public async Task<IEnumerable<NutritionTag>> GetOcrResponseFromDocument(IEnumerable<string> tagRequests)
        {
            List<NutritionTag> nutritionTags = new List<NutritionTag>();

            foreach (string tagRequest in tagRequests)
            {
                float chInPortion = 0;
                float grPerPortion = 0;
                string textractResponse = await _apiAmazonService.GetChFromDocument(tagRequest);
                string chPattern = @"[Cc]arbohidratos:?\s*(\d+)\s*g";
                string tagResponse = "";

                string portionPatter = @"[Pp]orci[oó]n\s*(\d+)\s*g";

                // Llama a la función para extraer la cantidad de carbohidratos por porción
                MatchCollection chMatches = Regex.Matches(textractResponse, chPattern);

                // Llama a la función para extraer la cantidad de gramos por porción
                MatchCollection portionMatches = Regex.Matches(textractResponse, portionPatter);

                // Imprime todas las coincidencias encontradas de gr de porciones
                if (portionMatches.Count > 0)
                {
                    foreach (Match match in portionMatches)
                    {
                        string grDetected = match.Groups[1].Value;
                        grPerPortion = float.Parse(grDetected);
                    }
                }
                else
                {
                    tagResponse += ("No se encontró la cantidad de gramos por porción en el texto proporcionado. ");
                }

                // Imprime todas las coincidencias encontradas de ch
                if (chMatches.Count > 0)
                {
                    foreach (Match match in chMatches)
                    {
                        string quantityCarbohydrates = match.Groups[1].Value;
                        chInPortion = float.Parse(quantityCarbohydrates);
                    }
                }
                else
                {
                    tagResponse += ("No se encontró la cantidad de carbohidratos por porción en el texto proporcionado.");
                }

                NutritionTag carbohydratesText = new NutritionTag();
                carbohydratesText.CarbohydratesText = tagResponse;
                carbohydratesText.grPerPortion = grPerPortion;
                carbohydratesText.chInPortion = chInPortion;

                nutritionTags.Add(carbohydratesText);
            }

            return nutritionTags;
        }
    }
}
