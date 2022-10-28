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

                var accountId = dbContext.AuthDetails.Where(x => x.IsActive == true && x.Type == Common.Enums.CompanyTypeEnum.QB).Select(x => x.Id).FirstOrDefault();
                AuthToken authToken = new AuthToken()
                {
                    AccessToken = addTokenDTO.AccessToken,
                    AccountId = addTokenDTO.AccountId,
                    RefreshToken = addTokenDTO.RefreshToken,
                    ExpiresIn = addTokenDTO.AccessTokenExpireIn,
                    CreatedDate = DateTime.Now,
                    CreatedDateUTC = DateTime.UtcNow,
                    Type = "Bearer",
                    IsActive = true,
                };

                var previousQBActiveTokenId = dbContext.AuthTokens.Where(x => x.IsActive == true).Select(x => x.Id).FirstOrDefault();

                if (previousQBActiveTokenId != Guid.Empty)
                {                   
                    var rec = dbContext.AuthTokens.Where(x => x.Id == previousQBActiveTokenId).FirstOrDefault();
                    rec.IsActive = false;
                    dbContext.AuthTokens.Update(rec);
                    dbContext.AuthTokens.Add(authToken);
                    dbContext.SaveChanges();
                }
                else
                {
                    dbContext.AuthTokens.Add(authToken);
                    dbContext.SaveChanges();
                }
            }
            return true;
        }

        public TokenDTO GetLatestQbToken()
        {
            Guid latestQbAccessToken = _appDBContext.AuthDetails
                .Where(x => x.IsActive == true && x.Type == Common.Enums.CompanyTypeEnum.QB)
                .Select(x => x.Id)
                .FirstOrDefault();


            var token = _appDBContext.AuthTokens.Where(x => x.AccountId == latestQbAccessToken).FirstOrDefault() != null ?
                 _appDBContext.AuthTokens.Where(x => x.AccountId == latestQbAccessToken).FirstOrDefault() : new AuthToken();

            return new TokenDTO()
            {
                access_token = token.AccessToken,
                refresh_token = token.RefreshToken,
                expires_in = token.ExpiresIn,
                token_type = token.Type
            };
        }
    }
}