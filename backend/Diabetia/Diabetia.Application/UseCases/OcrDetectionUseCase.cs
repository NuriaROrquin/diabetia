using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;


namespace Diabetia.Application.UseCases
{
      public class OcrDetectionUseCase
    {
        private readonly IApiAmazonProvider _apiAmazonService;

        public OcrDetectionUseCase(IApiAmazonProvider apiAmazonService)
        {
            _apiAmazonService = apiAmazonService;
        }

        public async Task<NutritionTag> GetOcrResponseFromDocument(string ocrRequest)
        {
            string ocrResponse = "";
            float chInPortion = 0;
            float grPerPortion = 0;
            string textractResponse = await _apiAmazonService.GetChFromDocument(ocrRequest);
            string chPattern = @"[Cc]arbohidratos:?\s*(\d+)\s*g";

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
                    string cantidadCarbohidratos = match.Groups[1].Value;
                    grPerPortion = float.Parse(cantidadCarbohidratos);
                    ocrResponse += ("Cantidad de gramos por porción: " + cantidadCarbohidratos + ". ");
                }
            }
            else
            {
                ocrResponse += ("No se encontró la cantidad de gramos por porción en el texto proporcionado. ");
            }

            // Imprime todas las coincidencias encontradas de ch
            if (chMatches.Count > 0)
            {
                foreach (Match match in chMatches)
                {
                    string cantidadCarbohidratos = match.Groups[1].Value;
                    chInPortion = float.Parse(cantidadCarbohidratos);
                    ocrResponse +=("Cantidad de carbohidratos por porción: " + cantidadCarbohidratos + ". ");
                }
            }
            else
            {
                ocrResponse += ("No se encontró la cantidad de carbohidratos por porción en el texto proporcionado.");
            }

            NutritionTag carbohydratesText = new NutritionTag();

            carbohydratesText.CarbohydratesText = ocrResponse;
            carbohydratesText.grPerPortion = grPerPortion;
            carbohydratesText.chInPortion = chInPortion;
            


            return carbohydratesText;
        }
    }
}
