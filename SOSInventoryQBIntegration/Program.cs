using Serilog;
using SOSInventoryQBIntegration;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var Configuration = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                             .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                             .Build();
#region Logging

//builder.Host.UseSerilog((ctx, lc) => lc
//       .ReadFrom.Configuration(ctx.Configuration));

#endregion

var startup = new Startup(Configuration, builder.Environment);
startup.ConfigureServices(builder.Services);
var app = builder.Build();
startup.Configure(app);
app.Run();
