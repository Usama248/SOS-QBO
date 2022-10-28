using Common.DTOs;
using Common.DTOs.QB.Auth;
using Common.DTOs.Shared;

namespace Application.IServices
{
    public interface IQBAuthService
    {
        string GetAuthorizationURL();
        
        ResponseDTO<AuthDetailsDTO> AddQBAuthCode(QBAddAuthCodeDTO qBAddAuthCodeDTO);
        
        bool AddToken(AddTokenDTO addTokenDTO);
        
        public Task GetAuthTokensAsync(string code, string realmId, Guid AccountId);

        public TokenDTO GetLatestQbAccessToken();

    }
}
