using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.EventRequest.FreeNote
{
    public class EditFreeNoteRequest : BasicEventRequest
    {
        public int EventId { get; set; }
        public string FreeNote { get; set; }

        public CargaEvento ToDomain()
        {
            var newFreeNoteEvent = new CargaEvento()
            {
                Id = EventId,
                FechaEvento = EventDate,
                NotaLibre = FreeNote
            };
            return newFreeNoteEvent;
        }
    }
}
