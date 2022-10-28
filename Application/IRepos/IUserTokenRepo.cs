using Common.DTOs;
using Common.DTOs.Shared;

namespace Application.IRepos
{
    public interface IUserTokenRepo
    {
        public ResponseDTO<TokenDTO> AddToken(TokenDTO addTokenDTO);
        public TokenDTO GetLatestToken();
    }
}
