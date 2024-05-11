using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using System.Configuration;

namespace Infraestructura.Provider
{
    public class Cognito
    {
        private readonly AmazonCognitoIdentityProviderClient _cognitoClient;
        private readonly string _poolId = "us-east-2_jec9xbm7c";
        private readonly string _clientId = "182096272230";
        private readonly RegionEndpoint _region = RegionEndpoint.USEast2;
        private string awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        private string awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

        public Cognito(string poolId, string clientId, RegionEndpoint region)
        {
            _poolId = poolId;
            _clientId = clientId;
            _region = region;

            _cognitoClient = new AmazonCognitoIdentityProviderClient(region);
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

        // Este metodo registra usuario usuario
    }
}
