using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Diabetia.Domain.Services;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Provider
{
    public class AuthProvider : IAuthProvider
    {
        private readonly IConfiguration _configuration;
        private readonly AmazonCognitoIdentityProviderClient _cognitoClient;
        private CognitoUserPool _cognitoUserPool;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public AuthProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            string _region = _configuration["Region"];
            string _userPoolId = _configuration["UserPoolId"];
            string awsAccessKey = _configuration["awsAccessKey"];
            string awsSecretKey = _configuration["awsSecretKey"];
            _clientId = _configuration["ClientId"];
            _clientSecret = _configuration["ClientSecret"];

            RegionEndpoint regionEndpoint = RegionEndpoint.GetBySystemName(_region);
            if (regionEndpoint == null)
            {
                throw new ArgumentException("La región especificada en la configuración no es válida.");
            }

            var credentials = new Amazon.Runtime.BasicAWSCredentials(awsAccessKey, awsSecretKey);
            AmazonCognitoIdentityProviderConfig clientConfig = new AmazonCognitoIdentityProviderConfig();
            clientConfig.RegionEndpoint = regionEndpoint;

            //var credentials = new Amazon.Runtime.BasicAWSCredentials(awsAccessKey, awsSecretKey);
            //AmazonCognitoIdentityProviderConfig clientConfig = new AmazonCognitoIdentityProviderConfig();
            //clientConfig.RegionEndpoint = _region;

            _cognitoClient = new AmazonCognitoIdentityProviderClient(credentials, clientConfig);
            _cognitoUserPool = new CognitoUserPool(_userPoolId, _clientId, _cognitoClient, _clientSecret); 

        }

        public async Task<string> RegisterUserAsync(string username, string password, string email)
        {
            
            var userAttributes = new Dictionary<string, string> {
                    { "email", email }
                };
            string secretHash = CalculateSecretHash(_clientId, _clientSecret, username);

            try
            {
                await _cognitoUserPool.SignUpAsync(username, password, userAttributes,  null);

                return secretHash;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar usuario: " + ex.Message);
            }
        }

        public async Task<bool> ConfirmEmailVerificationAsync(string username, string hashCode, string confirmationCode)
        {
            var request = new ConfirmSignUpRequest
            {
                ClientId = _clientId,
                Username = username,
                ConfirmationCode = confirmationCode,
                SecretHash = hashCode
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

        public async Task<string> LoginUserAsync(string username, string password)
        {
            string secretHash = CalculateSecretHash(_clientId, _clientSecret, username);
            var request = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                AuthParameters = new Dictionary<string, string>
                {
                    { "USERNAME", username },
                    { "PASSWORD", password },
                    { "SECRET_HASH", secretHash }
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

        public async Task ForgotPasswordRecoverAsync(string username)
        {
            string secretHash = CalculateSecretHash(_clientId, _clientSecret, username);
            var request = new ForgotPasswordRequest
            {
                ClientId = _clientId,
                Username =  username,
                SecretHash = secretHash
            };
            try
            {
                var response = await _cognitoClient.ForgotPasswordAsync(request);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("El correo con el código de verificación se ha enviado correctamente.");
                }
                else
                {
                    Console.WriteLine("Ocurrió un error al intentar enviar el correo.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al recuperar contraseña: {ex.Message}");
                throw;
            }
        }

        public async Task ConfirmForgotPasswordCodeAsync(string username, string confirmationCode, string password)
        {
            var request = new ConfirmForgotPasswordRequest
            {
                ClientId = _clientId,
                Username = username,
                ConfirmationCode = confirmationCode,
                Password = password,
                //secretHash
            };
            try
            {
                await _cognitoClient.ConfirmForgotPasswordAsync(request);
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message); 
            }
            
        }

        private string CalculateSecretHash(string userPoolClientId, string userPoolClientSecret, string userName)
        {
            const string HMAC_SHA256_ALGORITHM = "HMACSHA256";

            byte[] keyBytes = Encoding.UTF8.GetBytes(userPoolClientSecret);
            byte[] messageBytes = Encoding.UTF8.GetBytes(userName + userPoolClientId);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
