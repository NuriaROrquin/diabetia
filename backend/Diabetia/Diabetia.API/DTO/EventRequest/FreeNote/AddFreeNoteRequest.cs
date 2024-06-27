using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.EventRequest.FreeNote
{
    public class AddFreeNoteRequest : BasicEventRequest
    {
        public string FreeNote { get; set; }

        public int KindEventId { get; set; }

        public CargaEvento ToDomain()
        {
            var freeNoteEvent = new CargaEvento();
            freeNoteEvent.FechaEvento = EventDate;
            freeNoteEvent.NotaLibre = FreeNote;
            freeNoteEvent.IdTipoEvento = KindEventId;
            return freeNoteEvent;
        }
    } 
}
