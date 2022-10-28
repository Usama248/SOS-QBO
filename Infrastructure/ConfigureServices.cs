using Application.IRepos;
using Application.IServices;
using Infrastructure.Repos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
        {
            #region Repos

            services.AddScoped<IUserAuthRepo, UserAuthRepo>();
            services.AddScoped<IUserTokenRepo, UserTokenRepo>();
            services.AddScoped<IQBAuthRepo, QBAuthRepo>();
            services.AddScoped<IQBTokenRepo, QBTokenRepo>();

            #endregion Repos

            #region Services

            services.AddScoped<ISOSAuthService, SOSAuthService>();
            services.AddScoped<IQBAuthService, QBAuthService>();

            #endregion Services

            return services;
        }
    }
}
