using Common.DTOs;
using Common.DTOs.QB.Auth;
using Common.DTOs.Shared;

namespace Application.IServices
{
    public interface IQBAuthService
    {
        string GetAuthorizationURL();
        
        ResponseDTO<AuthDetailsDTO> AddQBAuthCode(QBAddAuthCodeDTO qBAddAuthCodeDTO);
        public Task GetAuthTokensAsync(string code, string realmId);
        public TokenDTO GetLatestQbAccessToken();

    }
}
