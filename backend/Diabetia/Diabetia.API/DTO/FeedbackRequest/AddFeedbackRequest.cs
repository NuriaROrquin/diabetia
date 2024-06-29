using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.FeedbackRequest
{
    public class AddFeedbackRequest
    {
        public int EventId { get; set; }

        public int SentimentalId { get; set; }

        public string FreeNote { get; set; }
        
        public Feedback toDomain()
        {
            var feedback = new Feedback()
            {
                IdSentimiento = SentimentalId,
                NotaLibre = FreeNote,
                IdCargaEventoNavigation = new CargaEvento()
                {
                    Id = EventId,
                }
            };
            return feedback;
        }
    }
}
