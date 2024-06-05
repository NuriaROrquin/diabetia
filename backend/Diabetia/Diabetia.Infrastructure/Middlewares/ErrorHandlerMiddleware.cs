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
