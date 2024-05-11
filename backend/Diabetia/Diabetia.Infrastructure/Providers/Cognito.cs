using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using System;
using System.Collections.Generic;

namespace Diabetia.Infrastructure.Providers
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Credenciales de tu aplicación Cognito
            string UserPoolId = "us-east-2_jec9xbm7c";
            string ClientId = "182096272230";

            // Configuración de la región de AWS
            var region = RegionEndpoint.USWest2; // Cambia esto a tu región específica

            // Datos de inicio de sesión del usuario
            string username = "FacuFagnano";
            string password = "diabetiaDEV1";

            // Configurar el proveedor de identidad de Cognito
            var provider = new AmazonCognitoIdentityProviderClient(region);

            // Configurar el objeto de autenticación de Cognito
            var userPool = new CognitoUserPool(UserPoolId, ClientId, provider);

            // Iniciar sesión con el nombre de usuario y la contraseña
            var user = new CognitoUser(username, ClientId, userPool, provider);
            var authRequest = new InitiateSrpAuthRequest()
            {
                Password = password
            };

            try
            {
                var authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
                Console.WriteLine("Inicio de sesión exitoso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al iniciar sesión: {ex.Message}");
            }
        }
    }
}