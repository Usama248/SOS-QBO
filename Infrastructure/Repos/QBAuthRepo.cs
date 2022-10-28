using Application.IRepos;
using Common.DTOs.QB;
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
        public AddQBResponseDTO Add(QBAddAuthCodeDTO qBAddAuthCodeDTO)
        {
            Account account = new Account()
            {
                 Code = qBAddAuthCodeDTO.AuthCode,
                 CompanyId = qBAddAuthCodeDTO.RealMId,
                 CompanyName = qBAddAuthCodeDTO.RealMId,
                 CreatedDate = DateTime.Now,
                 CreatedDateUTC = DateTime.UtcNow,
                 IsActive = true,
                 IsConnected = true,
                 Type = (Common.Enums.CompanyTypeEnum)qBAddAuthCodeDTO.Type
            };

            var previousQbActiveRecord = _dbContext
                .AuthDetails.Where(x => x.IsActive == true && x.Type == Common.Enums.CompanyTypeEnum.QB)
                .FirstOrDefault();

            AddQBResponseDTO addQBResponseDTO = new AddQBResponseDTO();


            if (previousQbActiveRecord == null)
            {
                _dbContext.AuthDetails.Add(account);
                addQBResponseDTO.DeletedQBId = Guid.Empty;
            }
            else
            {
                addQBResponseDTO.DeletedQBId = previousQbActiveRecord.Id;
                previousQbActiveRecord.IsActive = false;
                _dbContext.AuthDetails.Update(previousQbActiveRecord);
                _dbContext.AuthDetails.Add(account);
            }
            _dbContext.SaveChanges();
            addQBResponseDTO.AddedId = account.Id;


            return addQBResponseDTO;

        }

        public AuthCodeDTO GetLatestAuthDetails()
        {
            var latestActiveToken = _dbContext.AuthDetails.Where(x => x.IsActive == true && x.Type == Common.Enums.CompanyTypeEnum.QB).FirstOrDefault();
            if (latestActiveToken == null)
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

