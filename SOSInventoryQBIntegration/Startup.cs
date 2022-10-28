using SOSInventoryQBIntegration.Helpers;

namespace SOSInventoryQBIntegration
{
    public class Startup
    {
        public IWebHostEnvironment Env { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.RegisterServices(Configuration);
        }
        public void Configure(WebApplication app)
        {
            //app.UseExceptionLoggerMiddleware(
            //           options: new ExecptionLoggingOption
            //           {
            //               isDevelopment = Env.IsDevelopment()
            //           });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
