﻿namespace Diabetia.API.DTO.EventRequest
{
    public class InsulinEventRequest : EventRequest
    {
        public int? IdEvent { get; set; }
        public int IdKindEvent { get; set; }
        public string? FreeNote { get; set; }
        public int? Insulin { get; set; }
    }
}