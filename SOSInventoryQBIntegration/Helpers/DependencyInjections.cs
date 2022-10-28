using Common.DTOs.QB.Auth;
using Common.DTOs.SOS;
using Data.DBContext;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SOSInventoryQBIntegration.Helpers
{
    public static class DependencyInjections
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDBContext>(c => c.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();

            services.RegisterBusinessServices();

            #region [Configure App settings options]

            services.Configure<QBAuthDTO>(configuration.GetSection("QBCredentials"));
            services.Configure<SOSAppCredentialsDTO>(configuration.GetSection("SOSAppCredentials"));
            services.Configure<SOSAccessTokenCredentialsDTO>(configuration.GetSection("SOSAccessTokenCredentials"));
            services.Configure<QBAuthDTO>(configuration.GetSection("QBCredentials"));

            #endregion

            return services;
        }
    }
}
