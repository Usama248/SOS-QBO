using Application.IServices;
using Common.DTOs.QB.Auth;
using Microsoft.AspNetCore.Mvc;

namespace SOSInventoryQBIntegration.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly ISOSAuthService _sOSAuthService;
        private readonly IQBAuthService _qBAuthService;
        
        public AuthorizationController(ISOSAuthService sOSAuthService, IQBAuthService qBAuthService)
        {
            _sOSAuthService= sOSAuthService;
            _qBAuthService= qBAuthService;
        }

        public IActionResult SOSAuth([FromQuery] string code)
        {
            var value = code;
            var resp = _sOSAuthService.AddSOSAuthCode(value);                                               
            var latestAuthCode = _sOSAuthService.GetLatestAuthCode();                                       
            var Token = _sOSAuthService.GenerateAccessTokenFromAuthorizationCode(latestAuthCode.Code);      
            var addedToken = _sOSAuthService.AddTokens(Token, (Guid)resp.Data.Id);
            return View(resp);
        }

        public async Task<IActionResult> QBAuth([FromQuery] string code, string realmId)
        {

            QBAddAuthCodeDTO qBAddAuthCodeDTO = new QBAddAuthCodeDTO()
            {
                AuthCode = code,
                RealMId = realmId,
                Type = Common.Enums.CompanyTypeEnum.QB,
            };
            var authCodeResp = _qBAuthService.AddQBAuthCode(qBAddAuthCodeDTO);
            if (authCodeResp.Status is 200)
            {
                await _qBAuthService.GetAuthTokensAsync(code, realmId, authCodeResp.Data.Id.Value);  //id, code, compnay name
            }
            else { 
 
            }
            return View();
        }
    }
}
