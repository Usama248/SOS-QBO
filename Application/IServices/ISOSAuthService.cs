
using Common.DTOs;
using Common.DTOs.Shared;

namespace Application.IServices
{
    public interface ISOSAuthService
    {
        public string GetAuthCodeUrl();
        public ResponseDTO<AuthDetailsDTO> AddSOSAuthCode(string code);
        public AuthCodeDTO GetLatestAuthCode();
        public TokenDTO GenerateAccessTokenFromAuthorizationCode(string code);
        public ResponseDTO<TokenDTO> AddTokens(TokenDTO tokenDTO, Guid AccountId);

    }
}
