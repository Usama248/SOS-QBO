using Common.DTOs;
using Common.DTOs.Shared;
using Common.Enums;

namespace Application.IRepos
{
    public interface IUserAuthRepo
    {
        ResponseDTO<AuthDetailsDTO> AddAuthDetail(AuthDetailsDTO authDetailsDTO);
        public AuthCodeDTO GetLatestAuthCode(CompanyTypeEnum companyTypeEnum);
    }
}
