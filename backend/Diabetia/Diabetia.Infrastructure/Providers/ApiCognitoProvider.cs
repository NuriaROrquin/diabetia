﻿using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Diabetia.Domain.Services;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Net;

namespace Infrastructure.Provider
{
    public class ApiCognitoProvider : IApiCognitoProvider
    {
        private readonly AmazonCognitoIdentityProviderClient _cognitoClient;
        private readonly string _userPoolId = "us-east-2_jec9xbm7c";
        private readonly string _clientId = "7amuqfrhahhpgnfar29o1fk0u9";
        private readonly string _clientSecret = "71mbrja4bf4oveoa2cgl7bhtpnjp5p2e6h7gtu99ubeiohskks3";
        private readonly RegionEndpoint _region = RegionEndpoint.USEast2;
        private readonly IConfiguration _configuration;
        private CognitoUserPool _cognitoUserPool;

        // Constructor
        public ApiCognitoProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            string awsAccessKey = _configuration["awsAccessKey"];
            string awsSecretKey = _configuration["awsSecretKey"];
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