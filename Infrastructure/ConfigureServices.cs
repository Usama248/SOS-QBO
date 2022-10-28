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

            services.AddScoped<IUserTokenRepo, UserTokenRepo>();
            services.AddScoped<IQBTokenRepo, QBTokenRepo>();
            services.AddScoped<IQBAuthRepo, QBAuthRepo>();

            #endregion Repos

            #region Services

            services.AddScoped<ISOSAuthService, SOSAuthService>();
            services.AddScoped<IQBAuthService, QBAuthService>();
            services.AddScoped<IQBItemsService, QBItemsService>();
            services.AddScoped<ISSOItemsService, SSOItemsService>();
            services.AddScoped<IDataSyncing, DataSyncing>();



            services.AddScoped<ISyncService, SyncService > ();

            #endregion Services

            return services;
        }
    }
}
