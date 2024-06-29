
namespace Diabetia.Domain.Entities
{
    public class IngredientsDetected
    {
        public List<IngredientsRecognized> Ingredients { get; set; }
    }

    public class IngredientsRecognized
    {
        public double CarbohydratesPerPortion { get; set; }
        public double GrPerPortion { get; set; }
        public int FoodItemPosition { get; set; }
        public string[] Name { get; set; }
    }
}
