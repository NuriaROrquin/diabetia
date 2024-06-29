
namespace Diabetia.Domain.Entities
{
    public class IngredientsDetected
    {
        public List<IngredientsRecognized> Ingredients { get; set; }
    }

    public class IngredientsRecognized
    {
        public int CarbohydratesPerPortion { get; set; }
        public double GrPerPortion { get; set; }
        public int FoodItemPosition { get; set; }
    }
}
