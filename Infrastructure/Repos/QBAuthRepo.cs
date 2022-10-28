using Application.IRepos;
using Common.DTOs.QB.Auth;
using Common.DTOs.Shared;
using Data.DBContext;
using Data.Models;

namespace Infrastructure.Repos
{
    public class QBAuthRepo : IQBAuthRepo
    {
        AppDBContext _dbContext;
        public QBAuthRepo(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Add(QBAddAuthCodeDTO qBAddAuthCodeDTO)
        {
            Account account = new Account()
            {
                 Code = qBAddAuthCodeDTO.AuthCode,
                 CompanyName = qBAddAuthCodeDTO.RealMId,
                 CreatedDate = DateTime.Now,
                 CreatedDateUTC = DateTime.UtcNow,
                 IsActive = true,
                 IsConnected = true,
                 Type = qBAddAuthCodeDTO.Type
            };
           
            var previousQbActiveRecords = _dbContext.AuthDetails.Where(x => x.IsActive == true && x.Type == Common.Enums.CompanyTypeEnum.QB).ToList();

            if(previousQbActiveRecords.Count == 0)
            {
                _dbContext.AuthDetails.Add(account);
            } else
            {
                previousQbActiveRecords.ForEach(x => x.IsActive = false);
                _dbContext.AuthDetails.Add(account);
            }

            _dbContext.SaveChanges();
            return true;

        }

        public AuthCodeDTO GetLatestAuthDetails()
        {
            var latestActiveToken = _dbContext.AuthDetails.Where(x => x.IsActive == true && x.Type == Common.Enums.CompanyTypeEnum.QB).FirstOrDefault();
            if(latestActiveToken == null)
            {
                return new AuthCodeDTO() { };
            }
            return new AuthCodeDTO()
            {
                Id = latestActiveToken.Id,
                Type = Common.Enums.CompanyTypeEnum.QB,
                Code = latestActiveToken.Code,
                CompanyName = latestActiveToken.CompanyName,
            };
        }
    }
}
