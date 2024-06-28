using Diabetia.Domain.Entities;
using Diabetia.Domain.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Infrastructure.Providers
{
    public class FoodDishProvider : IFoodDishProvider
    {
        private readonly IFoodDishProvider _iFoodDishProvider;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public FoodDishProvider(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

        }

        public async Task<FoodDish> DetectFoodDish(Stream imageStream)
        {
            FoodDish result;
            string apiToken = _configuration["LogMealToken"];
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.logmeal.com/v2/image/segmentation/complete?language=esp");
            request.Headers.Add("Authorization", "Bearer " + apiToken);
            var content = new MultipartFormDataContent();

            content.Add(new StreamContent(imageStream), "image", "filename.jpg");

            request.Content = content;

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
            var foodDish  = JsonConvert.DeserializeObject<FoodDish>(responseBody);

            return foodDish;

            /* el Deserialize mapea CASI todo bien, pero no lo que más necesitamos
             Justo el Segmentation, que es la info que da las opciones de cada plato,
            no las puede mapear bien y por eso, queda un array vacío.
             */
        }

    }
}
