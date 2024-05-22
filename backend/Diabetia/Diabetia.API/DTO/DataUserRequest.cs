namespace Diabetia.API
{
    public class DataRequest
    {
        public string name { get; set; }
        public string birthdate { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public int weight { get; set; }
        public string lastname { get; set; }
        public int typeDiabetes { get; set; }
        public bool useInsuline { get; set; }
        public string typeInsuline { get; set; }
        public int idUser { get; set; }
    }


    public class PatientRequest
    {
        public string email { get; set; }
        public int weight { get; set; }
        public int typeDiabetes { get; set; }
        public bool useInsuline { get; set; }
        public string typeInsuline { get; set; }
    }



}
