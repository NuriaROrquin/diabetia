using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Diabetia.Domain.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Provider
{
    public class AuthProvider : IAuthProvider
    {
        private readonly IConfiguration _configuration;
        private IAmazonCognitoIdentityProvider _cognitoClient;
        //private readonly AmazonCognitoIdentityProviderClient _cognitoClient;
        private CognitoUserPool _cognitoUserPool;
        private readonly string _userPoolId;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _region;
        private readonly string _awsAccessKey;
        private readonly string _awsSecretKey;  

        public AuthProvider(IConfiguration configuration, IAmazonCognitoIdentityProvider cognitoClient)
        {
            _configuration = configuration;
            _cognitoClient = cognitoClient;
            _userPoolId = _configuration["UserPoolId"];
            _region = _configuration["Region"];
            _awsAccessKey = _configuration["awsAccessKey"];
            _awsSecretKey = _configuration["awsSecretKey"];
            _clientId = _configuration["ClientId"];
            _clientSecret = _configuration["ClientSecret"];
        }

        public async Task<string> RegisterUserAsync(string username, string password, string email)
        {
            var userAttributes = new Dictionary<string, string> {
                    { "email", email }
                };
            string secretHash = CalculateSecretHash(_clientId, _clientSecret, username);

            try
            {
                var _cognitoUserPool = CreateCognitoUserPool();
                //await _cognitoUserPool.SignUpAsync(username, password, userAttributes,  null);
                var request = new SignUpRequest
                {
                    ClientId = _clientId,
                    Username = username,
                    Password = password,
                    UserAttributes = userAttributes
                };
                await _cognitoClient.SignUpAsync(request);

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
            var response = await _cognitoClient.ConfirmSignUpAsync(request);
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
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
            string secretHash = CalculateSecretHash(_clientId, _clientSecret, username);
            var confirmRequest = new ConfirmForgotPasswordRequest
            {
                ClientId = _clientId,
                Username = username,
                ConfirmationCode = confirmationCode,
                Password = password,
                SecretHash = secretHash
            };
            try
            {
                await _cognitoClient.ConfirmForgotPasswordAsync(confirmRequest);
                var passwordRequest = new AdminSetUserPasswordRequest
                {
                    Password = password,
                    Username = username,
                    Permanent = true,
                    UserPoolId = _userPoolId,
                };
                await _cognitoClient.AdminSetUserPasswordAsync(passwordRequest);
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

        private CognitoUserPool CreateCognitoUserPool()
        {
            RegionEndpoint regionEndpoint = RegionEndpoint.GetBySystemName(_region);
            if (regionEndpoint == null)
            {
                throw new ArgumentException("La región especificada en la configuración no es válida.");
            }
            var credentials = new Amazon.Runtime.BasicAWSCredentials(_awsAccessKey, _awsSecretKey);
            AmazonCognitoIdentityProviderConfig clientConfig = new AmazonCognitoIdentityProviderConfig();
            clientConfig.RegionEndpoint = regionEndpoint;
            //var request = new CreateUserPoolClientRequest
            //{
            //    UserPoolId = _userPoolId;
            //Cognito
            //}
            //var request = new CreateUserPoolRequest
            //    {

            //    }
            _cognitoClient.
            _cognitoClient.CreateUserPoolAsync(CreateUserPoolRequest)
            _cognitoClient.CreateUserPoolClientAsync()
            //_cognitoClient = new AmazonCognitoIdentityProviderClient(credentials, clientConfig);
            _cognitoUserPool = new CognitoUserPool(_userPoolId, _clientId, _cognitoClient, _clientSecret);
            return _cognitoUserPool;

        }
    }
}
