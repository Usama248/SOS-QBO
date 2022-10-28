using Application.IRepos;
using Common.DTOs;
using Common.DTOs.Shared;
using Common.Enums;
using Data.DBContext;
using Data.Models;

namespace Infrastructure.Repos
{
    public class UserAuthRepo : IUserAuthRepo
    {
        AppDBContext _appDBContext;
        public UserAuthRepo(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public ResponseDTO<AuthDetailsDTO> AddAuthDetail(AuthDetailsDTO authDetailsDTO)
        {
            Account account = new Account()
            {
                Id = Guid.NewGuid(),
                Code = authDetailsDTO.Code,
                CreatedDate = DateTime.Now,
                CreatedDateUTC = DateTime.UtcNow,
                Type = authDetailsDTO.Type,
                IsActive = true,
                IsDeleted = false,
                IsConnected = false,
            };


            authDetailsDTO.Id = account.Id;
            authDetailsDTO.Type = account.Type;

            var previousActiveRecords = _appDBContext.AuthDetails.Where(x => x.IsActive == true && x.Type == authDetailsDTO.Type).ToList();

            if(previousActiveRecords.Count == 0)
            {
                _appDBContext.AuthDetails.Add(account);
            } else
            {
                previousActiveRecords.ForEach(x => x.IsActive = false);
                _appDBContext.AuthDetails.Add(account);
            }


            _appDBContext.SaveChanges();
            return new ResponseDTO<AuthDetailsDTO> { Data = authDetailsDTO, Message = "Added Successfully", Status = 200 };
        }

        public AuthCodeDTO GetLatestAuthCode(CompanyTypeEnum companyTypeEnum)
        {
            var authCode = _appDBContext.AuthDetails.Where(x => x.Type == companyTypeEnum && x.IsActive == true).FirstOrDefault();
            return new AuthCodeDTO()
            {
                Code = authCode.Code,
                CompanyName = authCode.CompanyName,
                Type = companyTypeEnum
            };
        }

    }
}
