using Common.DTOs;
using Common.DTOs.Shared;

namespace Application.IRepos
{
    public interface IUserTokenRepo
    {
        public ResponseDTO<AddTokenDTO> AddToken(AddTokenDTO addTokenDTO);
        public TokenDTO GetLatestToken();
    }
}
