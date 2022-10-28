using Application.IServices;
using Microsoft.AspNetCore.Mvc;
using SOSInventoryQBIntegration.Models;
using System.Diagnostics;

namespace SOSInventoryQBIntegration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISOSAuthService _sOSAuthService;
        private readonly IQBAuthService _qBAuthService;
        private readonly ISyncService _syncService;

        public HomeController(ILogger<HomeController> logger, ISOSAuthService sOSAuthService, IQBAuthService qBAuthService, ISyncService syncService)
        {
            _logger = logger;
            _syncService = syncService;
            _sOSAuthService = sOSAuthService;
            _qBAuthService = qBAuthService;

        }

        [HttpGet("Home/QBCallback")]
        public IActionResult QBAuthentication()
        {
            var url = _qBAuthService.GetAuthorizationURL();
            return Redirect(url);
        }

        [HttpGet("SOSAuthentication")]
        public IActionResult SOSAuthentication()
        {
            var url = _sOSAuthService.GetAuthCodeUrl();
            return Redirect(url);
        }

        public IActionResult Index()
        {
            var connection = _syncService.IsBothTypesLoggedIn();

            ViewBag.qbConnection = connection.qbConnection;
            ViewBag.sosConnection = connection.sosConnection;

            return View();
        }


        public IActionResult Sync()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}