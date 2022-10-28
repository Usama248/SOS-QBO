using Common.DTOs.QB.Auth;
using Common.DTOs.Shared;

namespace Application.IRepos
{
    public interface IQBAuthRepo
    {
        public bool Add(QBAddAuthCodeDTO qBAddAuthCodeDTO);
        public AuthCodeDTO GetLatestAuthDetails();

    }
}
