
using Common.DTOs;
using Common.DTOs.Shared;

namespace Application.IServices
{
    public interface ISOSAuthService
    {

        //Get the code to redirect the user.
        public string GetAuthCodeUrl();
        
        //Generate the Token
        public TokenDTO GenerateAccessTokenFromAuthorizationCode(string code);


        //Get Latest Token
        public TokenDTO GetLatestActiveAccessToken();

    }
}
