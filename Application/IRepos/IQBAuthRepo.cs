using Common.DTOs.QB;
using Common.DTOs.QB.Auth;
using Common.DTOs.Shared;

namespace Application.IRepos
{
    public interface IQBAuthRepo
    {
        public AddQBResponseDTO Add(QBAddAuthCodeDTO qBAddAuthCodeDTO);
        public AuthCodeDTO GetLatestAuthDetails();

    }
}
