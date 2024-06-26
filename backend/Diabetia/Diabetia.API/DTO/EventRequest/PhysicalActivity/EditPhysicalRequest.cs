﻿namespace Diabetia.API.DTO.EventRequest.PhysicalActivity
{
    public class EditPhysicalRequest : BasicEventRequest
    {
        public int EventId { get; set; }
        public int PhysicalActivity { get; set; }
        public TimeSpan IniciateTime { get; set; }
        public TimeSpan FinishTime { get; set; }
        public string FreeNote { get; set; }
    }
}