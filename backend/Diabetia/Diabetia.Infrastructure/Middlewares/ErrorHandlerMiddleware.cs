using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using Diabetia.Domain.Exceptions;
using Amazon.S3;

namespace Diabetia.Infrastructure.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var exceptionDetails = GetExceptionDetails(ex);
            context.Response.StatusCode = (int)exceptionDetails.StatusCode;
            var errorResponse = JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Message = exceptionDetails.Message
            });
            await context.Response.WriteAsync(errorResponse);
        }

        private (HttpStatusCode StatusCode, string Message) GetExceptionDetails(Exception ex)
        {
            var exceptionMap = new Dictionary<Type, (HttpStatusCode StatusCode, string Message)>
        {
            { typeof(UsernameExistsException), (HttpStatusCode.Conflict, "El usuario ya está registrado") }, // key = nameException ; value = (statusCode + message) -> tuple
            { typeof(NotAuthorizedException), (HttpStatusCode.BadRequest, "Usuario o contraseña incorrectos") },
            { typeof(InvalidPasswordException), (HttpStatusCode.BadRequest, "Contraseña inválida") },
            { typeof(InvalidParameterException), (HttpStatusCode.BadRequest, "Parámetros de solicitud inválidos") },
            { typeof(ExpiredCodeException), (HttpStatusCode.BadRequest, "El código ingresado ha expirado") },
            { typeof(UserNotFoundException), (HttpStatusCode.NotFound, "Usuario no encontrado") },
            { typeof(CodeMismatchException), (HttpStatusCode.BadRequest, "El código ingresado es incorrecto") },
            { typeof(InvalidEmailException), (HttpStatusCode.BadRequest, "El email no tiene un formato válido") },
            { typeof(UsernameNotFoundException), (HttpStatusCode.BadRequest, "Nombre de usuario no encontrado") },
            { typeof(InvalidOperationException), (HttpStatusCode.BadRequest, "Operación inválida") },
            { typeof(EmailAlreadyExistsException), (HttpStatusCode.BadRequest, "El email ya se encuentra registrado") },
            { typeof(UserNotConfirmedException), (HttpStatusCode.BadRequest, "El usuario aún no se encuentra confirmado") },
            { typeof(UserNotAuthorizedException), (HttpStatusCode.BadRequest, "El usuario no se encuentra autenticado") },
            { typeof(TooManyRequestsException), (HttpStatusCode.BadRequest, "La contraseña ingresada no es válida") },
            { typeof(LimitExceededException), (HttpStatusCode.BadRequest, "La contraseña ingresada no es válida") },
            { typeof(TooManyFailedAttemptsException), (HttpStatusCode.BadRequest, "Alcanzó el número máximo de intentos") },
            { typeof(PasswordResetRequiredException), (HttpStatusCode.BadRequest, "La contraseña ingresada caducó, necesita renovarla") },
            { typeof(NoInformationUserException), (HttpStatusCode.BadRequest, "Información del usuario no encontrada") },
            { typeof(Amazon.Runtime.Internal.HttpErrorResponseException), (HttpStatusCode.BadRequest, "Usuario o contraseña incorrectos") },
            { typeof(EventNotFoundException), (HttpStatusCode.BadRequest, "Evento no encontrado") },
            { typeof(UserEventNotFoundException), (HttpStatusCode.BadRequest, "El usuario no tiene asociado el evento seleccionado") },
            { typeof(PatientNotFoundException), (HttpStatusCode.BadRequest, "El usuario todavía no fue dado de alta como paciente") },
            { typeof(EventNotRelatedWithPatientException), (HttpStatusCode.BadRequest, "El evento no se encuentra relacionado al paciente") },
            { typeof(PhysicalEventNotMatchException), (HttpStatusCode.BadRequest, "La actividad física seleccionada es errónea") },
            { typeof(MismatchUserPatientException), (HttpStatusCode.BadRequest, "El usuario no posee el evento asociado") },
            { typeof(UserNotFoundOnDBException), (HttpStatusCode.BadRequest, "El usuario no se encuentra registrado") },
            { typeof(PatientInsulinRelationNotFoundException), (HttpStatusCode.BadRequest, "El paciente no se encuentra relacionado a esta insulina") },
            { typeof(GlucoseEventNotMatchException), (HttpStatusCode.BadRequest, "La carga de glucosa seleccionada es errónea") },
            { typeof(InsulinEventNotMatchException), (HttpStatusCode.BadRequest, "La carga de insulina seleccionada es errónea") },
            { typeof(FoodEventNotMatchException), (HttpStatusCode.BadRequest, "El registro de comida seleccionado es erróneo") },
            { typeof(IngredientFoodRelationNotFoundException), (HttpStatusCode.BadRequest, "Este ingrediente no se encuentra relacionado con ningún evento de comida") },
            { typeof(CantCreatObjectS3Async), (HttpStatusCode.BadRequest, "No se pudo guardar su archivo PDF correctamente") },
            { typeof(ExaminationEventNotFoundException), (HttpStatusCode.BadRequest, "Este evento no se encuentra relacionado con ningún estudio médico") },
            { typeof(CantDeleteObjectS3Async), (HttpStatusCode.BadRequest, "No se pudo eliminar su archivo PDF correctamente") },
            { typeof(AmazonS3Exception), (HttpStatusCode.BadRequest, "Error al intentar subir el archivo al servidor") },
            {typeof (GrPerPortionNotFoundException), (HttpStatusCode.BadRequest, "No se encontró la cantidad de gramos por porción en el texto proporcionado.") },
            { typeof(ChPerPortionNotFoundException),(HttpStatusCode.BadRequest, "No se encontró la cantidad de carbohidratos por porción en el texto proporcionado.")  }

        };

            if (exceptionMap.TryGetValue(ex.GetType(), out var details)) // details va a guardar el código de estado + el mensaje (si es que existe)
            {
                return details;
            }
            return (HttpStatusCode.InternalServerError, "Ocurrió un error inesperado");
        }

    } 
}
