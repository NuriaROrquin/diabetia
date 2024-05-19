namespace Diabetia.API.DTO
{
    public class TagRegistrationResponse
    {
        public string Id { get; set; }

        public float Portion { get; set; }

        public float GrPerPortion { get; set; }

        public float ChInPortion { get; set; }

        public float ChCalculated { get; set; }

        //chCalculated es la cantidad de carbohidratos consumidos, según la confirmación del usuario de la lectura del OCR
        //multiplicado por la porción que indicó que ingerió.
    }
}
