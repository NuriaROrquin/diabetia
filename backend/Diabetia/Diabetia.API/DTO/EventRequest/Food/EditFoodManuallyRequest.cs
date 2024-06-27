using Diabetia.Domain.Entities;
using Diabetia.Domain.Models;
using Diabetia.Domain.Utilities;

namespace Diabetia.API.DTO.EventRequest.Food
{
    public class EditFoodManuallyRequest : BasicEventRequest
    {
        public int EventId { get; set; }

        public string? FreeNote { get; set; }

        public IEnumerable<Ingredient>? Ingredients { get; set; }

        public EventoComidum ToDomain()
        {

            var ingredientFoodList = Ingredients.Select(ingredient => new IngredienteComidum
            {
                IdIngrediente = ingredient.IdIngredient,
                CantidadIngerida = (int)ingredient.Quantity,
            }).ToList();

            var cargaEvento = new CargaEvento
            {
                Id = EventId,
                FechaEvento = EventDate,
                FechaActual = DateTime.Now,
                NotaLibre = !string.IsNullOrEmpty(FreeNote) ? FreeNote : null,
                FueRealizado = EventDate <= DateTime.Now,
                EsNotaLibre = false
            };

            var foodEvent = new EventoComidum
            {
                IdCargaEventoNavigation = cargaEvento,
                IdTipoCargaComida = (int)FoodChargeTypeEnum.MANUAL,
            };

            foreach (var ingredientComidum in ingredientFoodList)
            {
                ingredientComidum.IdEventoComidaNavigation = foodEvent;
            }

            cargaEvento.EventoComida.Add(foodEvent);
            foodEvent.IngredienteComida = ingredientFoodList;

            return foodEvent;
        }
    }
}
