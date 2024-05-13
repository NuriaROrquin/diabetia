using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using System.Configuration;
using System.Net;

namespace Infraestructura.Provider
{
    public class Cognito
    {
        private readonly AmazonCognitoIdentityProviderClient _cognitoClient;
        private readonly string _userPoolId = "us-east-2_jec9xbm7c";
        private readonly string _clientId = "182096272230";
        private readonly RegionEndpoint _region = RegionEndpoint.USEast2;
        //private string awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        //private string awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

        // Constructor
        public Cognito(string poolId, string clientId, RegionEndpoint region)
        {
            _userPoolId = poolId;
            _clientId = clientId;
            _region = region;

            _cognitoClient = new AmazonCognitoIdentityProviderClient(region);
        }


        // Este metodo registra usuario
        public async Task<string> RegisterUserAsync(string username, string password, string email)
        {
            var request = new SignUpRequest
            {
                ClientId = _clientId,
                Username = username,
                Password = password,
                UserAttributes = new List<AttributeType>
                        {
                            new AttributeType { Name = "email", Value = email }
                        }
            };

            try
            {
                var response = await _cognitoClient.SignUpAsync(request);
                if (response.UserConfirmed == false && response.UserSub != null)
                {
                    // Envia el código de verificación al correo electrónico
                    var confirmRequest = new AdminInitiateAuthRequest
                    {
                        UserPoolId = _userPoolId,
                        ClientId = _clientId,
                        AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                        AuthParameters = new Dictionary<string, string>
                            {
                                {"USERNAME", username },
                                {"PASSWORD", password }
                            }
                    };

                    var confirmResponse = await _cognitoClient.AdminInitiateAuthAsync(confirmRequest);
                    return response.UserSub;
                }

                return response.UserSub;
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
