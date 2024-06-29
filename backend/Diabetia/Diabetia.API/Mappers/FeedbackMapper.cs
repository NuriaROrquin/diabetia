using Diabetia.API.DTO.FeedbackResponse;

namespace Diabetia.API.Mappers
{
    public static class FeedbackMapper
    {
        public static object MapToDTO(Dictionary<string, object> eventDict)
        {
            if (eventDict.ContainsKey("Title") && eventDict["Title"].ToString() == "Comida")
            {
                return new FoodEventResponse
                {
                    EventDate = ((DateTime)eventDict["EventDate"]).ToString("dd/MM/yyyy"),
                    Carbohydrates = (decimal)eventDict["Carbohydrates"],
                    EventId = (int)eventDict["IdEvent"],
                    KindEventId = (int)eventDict["KindEventId"]
                };
            }
            else if (eventDict.ContainsKey("Title") && eventDict["Title"].ToString() == "Actividad Física")
            {
                return new PhysicalActivityResponse
                {
                    EventDate = ((DateTime)eventDict["EventDate"]).ToString("dd/MM/yyyy"),
                    ActivityName = eventDict["ActivityName"].ToString(),
                    EventId = (int)eventDict["IdEvent"],
                    KindEventId = (int)eventDict["KindEventId"]

                };
            }
            throw new InvalidOperationException("Tipo de evento no reconocido para mapeo a DTO.");
        }
    }
}
