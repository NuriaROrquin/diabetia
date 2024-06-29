using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.FeedbackRequest
{
    public class AddFeedbackRequest
    {
        public int EventId { get; set; }
        public int FeelingId { get; set; }

        public string? FreeNote { get; set; }

        public AddFeedbackRequest ToDomain()
        {
            var feedback = new Feedback();

            feedback.IdCargaEvento = EventId;
            feedback.IdSentimiento = FeelingId;
            feedback.NotaLibre = FreeNote;

            return feedback;
        }
    }
}
