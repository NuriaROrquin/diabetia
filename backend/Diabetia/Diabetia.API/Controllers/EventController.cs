﻿using Amazon.Runtime.Internal;
using Diabetia.API.DTO;
using Diabetia.API.DTO.EventRequest;
using Diabetia.API.DTO.EventRequest.MedicalVisit;
using Diabetia.API.DTO.EventRequest.PhysicalActivity;
using Diabetia.Application.UseCases;
using Diabetia.Application.UseCases.EventUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventPhysicalActivityUseCase _eventPhysicalActivityUseCase;
        private readonly EventGlucoseUseCase _eventGlucosetUseCase;
        private readonly EventInsulinUseCase _eventInsulintUseCase;
        private readonly EventFoodUseCase _eventFoodManuallyUseCase;
        private readonly EventUseCase _getEventUseCase;
        private readonly DataUserUseCase _dataUserUseCase;
        private readonly EventMedicalExaminationUseCase _eventMedicalExaminationUseCase;
        private readonly EventMedicalVisitUseCase _eventMedicalVisitUseCase;

        public EventController(EventPhysicalActivityUseCase eventPhysicalActivityUseCase, EventGlucoseUseCase eventGlucoseUseCase, EventInsulinUseCase eventInsulinUseCase, EventFoodUseCase eventFoodManuallyUseCase, EventUseCase eventUseCase, DataUserUseCase dataUserUseCase, EventMedicalExaminationUseCase eventMedicalExaminationUseCase, EventMedicalVisitUseCase eventMedicalVisitUseCase)
        {
            _eventPhysicalActivityUseCase = eventPhysicalActivityUseCase;
            _eventGlucosetUseCase = eventGlucoseUseCase;
            _eventInsulintUseCase = eventInsulinUseCase;
            _eventFoodManuallyUseCase = eventFoodManuallyUseCase;
            _getEventUseCase = eventUseCase;
            _dataUserUseCase = dataUserUseCase;
            _eventMedicalExaminationUseCase = eventMedicalExaminationUseCase;
            _eventMedicalVisitUseCase = eventMedicalVisitUseCase;
        }

        [HttpPost("AddPhysicalEvent")]
        [Authorize]
        public async Task <IActionResult> AddPhysicalEvent([FromBody] AddPhysicalRequest request)
        {
            await _eventPhysicalActivityUseCase.AddPhysicalEventAsync(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.PhysicalActivity, request.IniciateTime, request.FinishTime);
            return Ok("Evento creado correctamente");
        }

        [HttpPost("EditPhysicalEvent")]
        [Authorize]
        public async Task<IActionResult> EditPhysicalEvent([FromBody] EditPhysicalRequest request)
        {
            await _eventPhysicalActivityUseCase.EditPhysicalEventAsync(request.Email, request.EventId, request.EventDate, request.PhysicalActivity, request.IniciateTime, request.FinishTime, request.FreeNote);
            return Ok("Evento modificado correctamente"); ;
        }


        [HttpPost("AddGlucoseEvent")]
        public async Task<IActionResult> AddGlucoseEvent([FromBody] EventGlucoseRequest request)
        {
            await _eventGlucosetUseCase.AddGlucoseEvent(request.Email, request.IdKindEvent.Value, request.EventDate, request.FreeNote, request.Glucose.Value, request.IdFoodEvent, request.PostFoodMedition);
            return Ok();
        }

        [HttpPost("EditGlucoseEvent")]
        public async Task<IActionResult> EditGlucoseEvent([FromBody] EventGlucoseRequest request)
        {
            await _eventGlucosetUseCase.EditGlucoseEvent(request.IdEvent.Value, request.Email, request.EventDate, request.FreeNote, request.Glucose.Value, request.IdFoodEvent, request.PostFoodMedition);
            return Ok("Evento modificado correctamente");
        }


        [HttpPost("AddInsulinEvent")]
        public async Task<IActionResult> AddInsulinEvent([FromBody] EventInsulinRequest request)
        {
            await _eventInsulintUseCase.AddInsulinEvent(request.Email, request.IdKindEvent.Value, request.EventDate, request.FreeNote, request.Insulin.Value);
            return Ok();
        }

        [HttpPost("EditInsulinEvent")]
        public async Task<IActionResult> EditInsulinEvent([FromBody] EventInsulinRequest request)
        {
            await _eventInsulintUseCase.EditInsulinEvent(request.IdEvent.Value, request.Email, request.EventDate, request.FreeNote, request.Insulin.Value);
            return Ok("Evento modificado correctamente");
        }

        [HttpPost("AddFoodManuallyEvent")]
        public async Task<EventFoodResponse> AddFoodManuallyEvent([FromBody] EventFoodRequest request)
        {
            EventFoodResponse response = new EventFoodResponse();
           var totalChConsumed = await _eventFoodManuallyUseCase.AddFoodManuallyEvent(request.Email, request.EventDate, request.IdKindEvent.Value, request.Ingredients, request.FreeNote);

            var userPatientInfo = await _dataUserUseCase.GetPatientInfo(request.Email);
            if (userPatientInfo.ChCorrection != null)
            {
                var insulinToCorrect = totalChConsumed / userPatientInfo.ChCorrection;
                response.InsulinToCorrect = (float)insulinToCorrect;
            }

            response.ChConsumed = (int)totalChConsumed;

            return response;
        }

        [HttpPost("EditFoodManuallyEvent")]
        public async Task<IActionResult> EditFoodManuallyEvent([FromBody] EventFoodRequest request)
        {
            await _eventFoodManuallyUseCase.EditFoodManuallyEvent(request.IdEvent.Value, request.Email, request.EventDate, request.IdKindEvent.Value, request.Ingredients, request.FreeNote);
            return Ok();
        }

        [HttpPost("AddMedicalExaminationEvent")]
        public async Task<IActionResult> AddMedicalExaminationEvent([FromBody] EventMedicalExaminationRequest request)
        {
            await _eventMedicalExaminationUseCase.AddMedicalExaminationEvent(request.Email, request.EventDate, request.File, request.ExaminationType, request.IdProfessional, request.FreeNote);
            return Ok();
        }

        // ------------------------------------ Medical Visit --------------------------------------------
        [HttpPost("AddMedicalVisitEvent")]
        public async Task<IActionResult> AddMedicalEventAsync([FromBody] AddMedicalVisitRequest request)
        {
            await _eventMedicalVisitUseCase.AddMedicalVisitEventAsync(request.Email, request.KindEventId, request.EventDate, request.ProfessionalId, request.Recordatory, request.RecordatoryDate, request.Description);
            return Ok("Visita médica agregada correctamente");
        }

        [HttpPost("EditMedicalVisitEvent")]
        public async Task<IActionResult> EditMedicalEventAsync([FromBody] EditMedicalVisitRequest request)
        {
            await _eventMedicalVisitUseCase.EditMedicalVisitEventAsync(request.Email, request.EventId, request.EventDate, request.ProfessionalId, request.Recordatory, request.RecordatoryDate, request.Description);
            return Ok("Visita médica modificada correctamente");
        }


        [HttpGet("GetEventType/{id}")]
        public async Task<IActionResult> GetEventType([FromRoute] int id)
        {
            var idEvent = id;
            var eventType = await _getEventUseCase.GetEvent(id);
            if (eventType == null)
            {
                return NotFound();
            }
            return Ok(eventType);
        }

        [HttpPost("DeleteEvent/{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            await _getEventUseCase.DeleteEvent(id);
            return Ok();
        }
        [HttpGet("GetIngredients")]
        public async Task<IngredientResponse> GetIngredients()
        {
            var ingredients = await _eventFoodManuallyUseCase.GetIngredients();

            var ingredientsMapped = new IngredientResponse
            {
                Ingredients = ingredients,
            };

            return ingredientsMapped;
        }
    }
}
