using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace Infraestructura.Provider
{
    public class ApiCognitoService
    {
        private readonly AmazonCognitoIdentityProviderClient _cognitoClient;
        private readonly string _userPoolId = "us-east-2_jec9xbm7c";
        private readonly string _clientId = "7amuqfrhahhpgnfar29o1fk0u9";
        private readonly RegionEndpoint _region = RegionEndpoint.USEast2;
        private CognitoUserPool _cognitoUserPool;
        //private string awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        //private string awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

        // Constructor
        public ApiCognitoService(RegionEndpoint region)
        {
            _region = region;


            _cognitoClient = new AmazonCognitoIdentityProviderClient(region);

            _cognitoUserPool = new CognitoUserPool(_userPoolId, _clientId, _cognitoClient);

        }


        public async Task RegisterUserAsync(string username, string password, string email)
        {

            var userAttributes = new Dictionary<string, string> {
                    { "email", email }
                };
            
            try
            {
                await _cognitoUserPool.SignUpAsync(username, password, userAttributes,  null);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar usuario: " + ex.Message);
            }
        }

        // Este metodo verifica el codigo dentro del correo del usuario
        public async Task<bool> ConfirmEmailVerificationAsync(string username, string confirmationCode)
        {
            var request = new ConfirmSignUpRequest
            {
                ClientId = _clientId,
                Username = username,
                ConfirmationCode = confirmationCode
            };

            try
            {
                var response = await _cognitoClient.ConfirmSignUpAsync(request);
                return response.HttpStatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al confirmar verificación de correo electrónico: " + ex.Message);
            }
        }

        // Este metodo autentica usuarios
        public async Task<string> AuthenticateUserAsync(string username, string password)
        {
            var request = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                AuthParameters = new Dictionary<string, string>
                {
                    { "USERNAME", username },
                    { "PASSWORD", password }
                },
                ClientId = _clientId
            };

            try
            {
                var response = await _cognitoClient.InitiateAuthAsync(request);
                return response.AuthenticationResult.AccessToken;
            }
            catch (NotAuthorizedException e)
            {
                throw new Exception("La autenticación ha fallado. Mensaje de error: " + e.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al autenticar al usuario: " + ex.Message);
            }
        }
    }
}
