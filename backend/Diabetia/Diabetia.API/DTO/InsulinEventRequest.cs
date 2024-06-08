﻿namespace Diabetia.API.DTO
{
    public class InsulinEventRequest
    {
        public int? IdEvent { get; set; }
        public string Email { get; set; }
        public int IdKindEvent { get; set; }
        public DateTime EventDate { get; set; }
        public string? FreeNote { get; set; }
        public int Insulin { get; set; }
    }
}