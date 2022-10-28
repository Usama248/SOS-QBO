using Application.IRepos;
using Common.DTOs.Shared;
using Data.DBContext;
using Data.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Repos
{
    public class QBTokenRepo : IQBTokenRepo
    {

        AppDBContext _appDBContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public QBTokenRepo(AppDBContext appDBContext, IServiceScopeFactory serviceScopeFactory)
        {
            _appDBContext = appDBContext;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public bool AddQbToken(AddTokenDTO addTokenDTO)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDBContext>();

                AuthToken authToken = new AuthToken()
                {
                    CompanyId = addTokenDTO.AccountId,
                    AccessToken = addTokenDTO.AccessToken,
                    RefreshToken = addTokenDTO.RefreshToken,
                    ExpiresIn = addTokenDTO.AccessTokenExpireIn,
                    CreatedDate = DateTime.Now,
                    CreatedDateUTC = DateTime.UtcNow,
                    CompanyTypeEnum = Common.Enums.CompanyTypeEnum.QB,
                    Type = "bearer",
                    IsActive = true,
                };

                var previousQBActiveTokenId = dbContext.AuthTokens
                    .Where(x => x.IsActive == true && x.CompanyTypeEnum == Common.Enums.CompanyTypeEnum.QB)
                    .Select(x => x.Id)
                    .FirstOrDefault();

                if (previousQBActiveTokenId != Guid.Empty)
                {
                    var rec = dbContext.AuthTokens.Where(x => x.Id == previousQBActiveTokenId).FirstOrDefault();
                    rec.IsActive = false;
                    dbContext.AuthTokens.Update(rec);
                    dbContext.AuthTokens.Add(authToken);
                }
                else
                {
                    dbContext.AuthTokens.Add(authToken);
                }
                dbContext.SaveChanges();

            }
            return true;
        }

        public TokenDTO GetLatestQbToken()
        {
            var token = _appDBContext.AuthTokens.Where(x => x.IsActive == true && x.CompanyTypeEnum == Common.Enums.CompanyTypeEnum.QB).FirstOrDefault();
            return new TokenDTO()
            {
                access_token = token.AccessToken,
                refresh_token = token.RefreshToken,
                expires_in = token.ExpiresIn,
                token_type = token.Type,
                company_type = Common.Enums.CompanyTypeEnum.QB,
            };
        }

        public (bool qbConnection, bool sosConnection) IsBothUsersLoggedIn()
        {
            var tokens = _appDBContext.AuthTokens
                .Where(x => x.IsActive == true && x.CompanyTypeEnum == Common.Enums.CompanyTypeEnum.SOS || x.CompanyTypeEnum == Common.Enums.CompanyTypeEnum.QB).ToList();

            return (tokens.Any(d => d.CompanyTypeEnum == Common.Enums.CompanyTypeEnum.QB), tokens.Any(d => d.CompanyTypeEnum == Common.Enums.CompanyTypeEnum.SOS));
        }
    }
}