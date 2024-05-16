﻿using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Diabetia.Domain.Services;
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
        private CognitoUserPool _cognitoUserPool;

        private string awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        private string awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

        //var builder = new ConfigurationBuilder().AddUserSecrets<Startup>();
        //var configuration = builder.Build();

        // Constructor
        public ApiCognitoProvider()
        {

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

        // Este metodo recupera la contraseña del usuario
        public async Task<string> ForgotPasswordAsync(string username)
        {
            try
            {

                var userPool = new CognitoUserPool(_userPoolId, _clientId, _cognitoClient, _clientSecret);

                var request = new ForgotPasswordRequest
                {
                    Username = username
                };

                var response = await _cognitoClient.ForgotPasswordAsync(request);

                return response.HttpStatusCode.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // Este confirma el codigo y la password nueva
        public async Task<string> ConfirmPasswordAsync(string username, string password, string confirmationCode)
        {
            try
            {
                var userPool = new CognitoUserPool(_userPoolId, _clientId, _cognitoClient, _clientSecret);
                var request = new ConfirmForgotPasswordRequest
                {
                    Username = username,
                    ClientId = _clientId,
                    Password = password,
                    ConfirmationCode = confirmationCode
                };

                var response = await _cognitoClient.ConfirmForgotPasswordAsync(request);
                return response.HttpStatusCode.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


    }
}
