﻿using Diabetia.API.DTO;
using Diabetia.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly AddPhysicalEventUseCase _addPhysicalEventUseCase;
        private readonly AddGlucoseEventUseCase _addGlucoseEventUseCase;
        private readonly AddInsulinEventUseCase _addInsulineEventUseCase;

        public EventController(AddPhysicalEventUseCase addPhysicalEventUseCase, AddGlucoseEventUseCase addGlucoseEventUseCase, AddInsulinEventUseCase addInsulineEventUseCase)
        {
            _addPhysicalEventUseCase = addPhysicalEventUseCase;
            _addGlucoseEventUseCase = addGlucoseEventUseCase;
            _addInsulineEventUseCase = addInsulineEventUseCase;
        }

        [HttpPost("AddPhysicalEvent")]
        public async Task <IActionResult> AddPhysicalEvent([FromBody] EventRequest request)
        {
            await _addPhysicalEventUseCase.AddPhysicalEvent(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.PhysicalActivity, request.IniciateTime, request.FinishTime);
            return Ok();
        }

        [HttpPost("AddGlucoseEvent")]
        public async Task<IActionResult> AddGlucoseEvent([FromBody] GlucoseEventRequest request)
        {
            await _addGlucoseEventUseCase.AddGlucoseEvent(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.Glucose, request.IdDevicePacient, request.IdFoodEvent, request.PostFoodMedition);
            return Ok();
        }

        [HttpPost("AddInsulinEvent")]
        public async Task<IActionResult> AddInsulinEvent([FromBody] InsulinEventRequest request)
        {
            await _addInsulineEventUseCase.AddInsulinEvent(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.Insulin);
            return Ok();
        }
    }
}
