using Common.DTOs.Shared;

namespace Application.IRepos
{
    public interface IQBTokenRepo
    {
        public bool AddQbToken(AddTokenDTO addTokenDTO);
        public TokenDTO GetLatestQbToken();

        public (bool qbConnection, bool sosConnection) IsBothUsersLoggedIn();


    }
}
