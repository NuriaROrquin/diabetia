using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using System.Text.RegularExpressions;
using Diabetia.Domain.Exceptions;
using Diabetia.Interfaces;


namespace Diabetia.Application.UseCases
{
    public class TagDetectionUseCase
    {
        private readonly ITagRecognitionProvider _apiAmazonService;
        private readonly IPatientValidator _patientValidator;

        public TagDetectionUseCase(ITagRecognitionProvider apiAmazonService, IPatientValidator patientValidator)
        {
            _apiAmazonService = apiAmazonService;
            _patientValidator = patientValidator;
        }

        public async Task<IEnumerable<NutritionTag>> GetOcrResponseFromDocument(string email, IEnumerable<string> tagRequests)
        {
            await _patientValidator.ValidatePatient(email);

            List<NutritionTag> nutritionTags = new List<NutritionTag>();

            foreach (var tagRequest in tagRequests)
            {
                NutritionTag nutritionTag = new NutritionTag();
                nutritionTag = await _apiAmazonService.GetChFromDocument(tagRequest);
                float chInPortion = 0;
                float grPerPortion = 0;
                string textractResponse = nutritionTag.CarbohydratesText;
                string chPattern = @"[Cc]arbohidratos.*?\s*(\d+)\s*g";

                string portionPatter = @"[Pp]orci[oó]n\s*[:=\s]*\s*(\d+)\s*g";

                MatchCollection chMatches = Regex.Matches(textractResponse, chPattern);

                MatchCollection portionMatches = Regex.Matches(textractResponse, portionPatter);

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
                    throw new GrPerPortionNotFoundException();
                }

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
                    throw new ChPerPortionNotFoundException();
                }

                NutritionTag carbohydratesText = new NutritionTag();
                carbohydratesText.GrPerPortion = grPerPortion;
                carbohydratesText.ChInPortion = chInPortion;
                carbohydratesText.UniqueId = nutritionTag.UniqueId;

                nutritionTags.Add(carbohydratesText);
            }



            return nutritionTags;
        }
    }
}
