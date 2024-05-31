using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
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

        private readonly string _userPoolId;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public AuthProvider(IConfiguration configuration, IAmazonCognitoIdentityProvider cognitoClient)
        {
            _configuration = configuration;
            _cognitoClient = cognitoClient;

            _clientId = _configuration["ClientId"];
            _clientSecret = _configuration["ClientSecret"];
            _userPoolId = _configuration["UserPoolId"];
        }

        public async Task<string> RegisterUserAsync(string username, string password, string email)
        {

            string secretHash = CalculateSecretHash(_clientId, _clientSecret, username);

            var request = new SignUpRequest
            {
                ClientId = _clientId,
                Password = password,
                SecretHash = secretHash,
                UserAttributes = new List<AttributeType>
                {
                    new AttributeType { Name = "email", Value = email}
                },
                Username = username
            };

                
            await _cognitoClient.SignUpAsync(request, CancellationToken.None).ConfigureAwait(true);

            return secretHash;

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

            return response.HttpStatusCode == HttpStatusCode.OK;
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

            var response = await _cognitoClient.InitiateAuthAsync(request);

            return response.AuthenticationResult.AccessToken;
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

            await _cognitoClient.ForgotPasswordAsync(request);
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

        public async Task ChangeUserPasswordAsync(string accessToken, string previousPassword, string newPassword)
        {
            var passwordRequest = new ChangePasswordRequest
            {
                AccessToken = accessToken,
                PreviousPassword = previousPassword,
                ProposedPassword = newPassword
            };
            await _cognitoClient.ChangePasswordAsync(passwordRequest);
        }

        public string CalculateSecretHash(string userPoolClientId, string userPoolClientSecret, string userName)
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
