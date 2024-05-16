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
    public class ApiCognitoProvider : IApiCognitoProvider
    {
        private readonly AmazonCognitoIdentityProviderClient _cognitoClient;
        private readonly string _userPoolId = "us-east-1_Ev8XQPSJv";
        private readonly RegionEndpoint _region = RegionEndpoint.USEast1;
        private readonly IConfiguration _configuration;
        private CognitoUserPool _cognitoUserPool;
        private readonly string _clientId;
        private readonly string _clientSecret;

        // Constructor
        public ApiCognitoProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            string awsAccessKey = _configuration["awsAccessKey"];
            string awsSecretKey = _configuration["awsSecretKey"];
            _clientId = _configuration["ClientId"];
            _clientSecret = _configuration["ClientSecret"];

            var credentials = new Amazon.Runtime.BasicAWSCredentials(awsAccessKey, awsSecretKey);
            AmazonCognitoIdentityProviderConfig clientConfig = new AmazonCognitoIdentityProviderConfig();
            clientConfig.RegionEndpoint = _region;

            _cognitoClient = new AmazonCognitoIdentityProviderClient(credentials, clientConfig);
            _cognitoUserPool = new CognitoUserPool(_userPoolId, _clientId, _cognitoClient, _clientSecret); 

        }


        public async Task<string> RegisterUserAsync(string username, string password, string email)
        {
            
            var userAttributes = new Dictionary<string, string> {
                    { "email", email }
                };

            
            //string message = username + _clientId;
            //byte[] secretBytes = Encoding.UTF8.GetBytes(_clientSecret);
            //var hmac = new HMACSHA256(secretBytes);
            //byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            //string secretHash = Convert.ToBase64String(hashBytes);
            string secretHash = CalculateSecretHash(_clientId, _clientSecret, username);
            try
            {
                await _cognitoUserPool.SignUpAsync(username, password, userAttributes,  null);
                return "success";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar usuario: " + ex.Message);
            }
        }
        
        static string CalculateSecretHash(string userPoolClientId, string userPoolClientSecret, string userName)
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





        // Este metodo verifica el codigo dentro del correo del usuario
        public async Task<bool> ConfirmEmailVerificationAsync(string username, string confirmationCode)
        {
            var request = new ConfirmSignUpRequest
            {
                ClientId = _clientId,
                Username = username,
                ConfirmationCode = confirmationCode,
                SecretHash = "8G/Ce23WTwSz4VZZ+CfnExiHHLsAdIy3lNobbaHc6w4="
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

        
        

// Este metodo loguea usuarios
public async Task<string> LoginUserAsync(string username, string password)
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
