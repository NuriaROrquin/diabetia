using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using Diabetia.Application.Exceptions;

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

            // Register Exceptions
            if (ex is UsernameExistsException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.Conflict, "El usuario ya está registrado");
            }
            else if (ex is NotAuthorizedException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "Usuario o contraseña incorrectos");
            }
            else if (ex is InvalidPasswordException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "Contraseña inválida");
            }
            else if (ex is InvalidParameterException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "Parámetros de solicitud inválidos");
            }
            else if (ex is ExpiredCodeException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "El código ingresado ha expirado");
            }
            else if (ex is UserNotFoundException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.NotFound, "Usuario no encontrado");
            }
            else if (ex is CodeMismatchException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "El código ingresado es incorrecto");
            }
            else if (ex is InvalidEmailException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "El email no tiene un formato válido");
            }
            else if (ex is UsernameNotFoundException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "Nombre de usuario no encontrado");
            }
            else if (ex is InvalidOperationException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "Operación inválida");
            }
            else if (ex is EmailAlreadyExistsException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "El email ya se encuentra registrado");
            }

            // Change Password Exceptions
            else if (ex is UserNotConfirmedException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "El usuario aún no se encuentra confirmado");
            }

            // Forgot Password Exceptions
            else if (ex is UserNotAuthorizedException) // Tambien del Login
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "El usuario no se encuentra autenticado");
            }
            else if (ex is TooManyRequestsException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "La contraseña ingresada no es válida");
            }
            else if (ex is LimitExceededException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "La contraseña ingresada no es válida");
            }

            // Confirm Forgot Password Exceptions
            else if (ex is TooManyFailedAttemptsException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "Alcanzó el número máximo de intentos");
            }
            // Login Exceptions
            else if (ex is PasswordResetRequiredException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "La contraseña ingresada caducó, necesita renovarla");
            }
            else if (ex is NoInformationUserException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "Información del usuario no encontrada");
            }
            else if (ex is Amazon.CognitoIdentityProvider.Model.NotAuthorizedException) // Asegúrate de que esta línea está correcta
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "Usuario o contraseña incorrectos");
            }
            else if (ex is Amazon.Runtime.Internal.HttpErrorResponseException httpEx &&
             httpEx.InnerException is Amazon.CognitoIdentityProvider.Model.NotAuthorizedException cognitoEx)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "Usuario o contraseña incorrectos");
            }

            // Event Exceptions
            else if (ex is EventNotFoundException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "Evento no encontrado");
            }
            else if (ex is UserEventNotFoundException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "El usuario no tiene asociado el evento seleccionado");
            }
            else if (ex is PatientNotFoundException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "El usuario todavía no fue dado de alta como paciente");
            }
            else if (ex is EventNotRelatedWithPatientException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "El evento no se encuentra relacionado al paciente");
            }
            else if (ex is PhysicalEventNotMatchException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "La actividad física seleccionada es erronea");
            }
            else if (ex is MismatchUserPatientException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "El usuario no posee el evento asociado");
            }
            else if (ex is UserNotFoundOnDBException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "El usuario no se encuentra registrado");

            }
            else if (ex is PatientInsulinRelationNotFoundException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "El paciente no se encuentra relacionado a esta insulina");
            }
            else if (ex is GlucoseEventNotMatchException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "La carga de glucosa seleccionada es errónea.");
            }
            else if (ex is InsulinEventNotMatchException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "La carga de insulina seleecionada es errónea.");
            }
            else if (ex is FoodEventNotMatchException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "El registro de comida seleecionado es errónea.");
            }
            else if (ex is IngredientFoodRelationNotFoundException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "Este ingrediente no se encuentra relacionado con ningún evento de comida.");
            }
            else if (ex is CantCreatObjectS3Async)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "No se pudo pudo guardar su archivo PDF correctamente.");
            }
            else if (ex is ExaminationEventNotFoundException)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "Este evento no se encuentra relacionado con ningún estudio médico.");
            }
            else if (ex is CantDeleteObjectS3Async)
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.BadRequest, "No se pudo pudo eliminar su archivo PDF correctamente.");
            }
            else
            {
                await HandleExceptionWithStatusCode(context, ex, HttpStatusCode.InternalServerError, "Este es un mensaje de error custom");
            }
        }

        private async Task HandleExceptionWithStatusCode(HttpContext context, Exception ex, HttpStatusCode statusCode, string message)
        {
            context.Response.StatusCode = (int)statusCode;

            var errorResponse = JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            });

            await context.Response.WriteAsync(errorResponse);
        }
    } 
}
