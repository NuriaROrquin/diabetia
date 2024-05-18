namespace Diabetia.API.DTO
{
    public class TagRegistrationResponse
    {
        public float portion { get; set; }
        public float grPerPortion { get; set; }
        public float chInPortion { get; set; }
        public float chCalculated { get; set; }
        //chCalculated es la cantidad de carbohidratos consumidos, según la confirmación del usuario de la lectura del OCR
        //multiplicado por la porción que indicó que ingerió.
    }
}
