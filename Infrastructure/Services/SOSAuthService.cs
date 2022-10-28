using Application.IRepos;
using Application.IServices;
using Common.DTOs;
using Common.DTOs.Shared;
using Common.DTOs.SOS;
using Common.Enums;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace Infrastructure.Services
{
    public class SOSAuthService : ISOSAuthService
    {
        private readonly IOptions<SOSAppCredentialsDTO> _SOSAppCredentialsDTO;
        private readonly IOptions<SOSAccessTokenCredentialsDTO> _SOSAccessTokenCredentialsDTO;

        private readonly IUserAuthRepo _userAuthRepo;
        private readonly IUserTokenRepo _userTokenRepo;


        public SOSAuthService(IOptions<SOSAppCredentialsDTO> SOSAppCredentialsDTO, IUserAuthRepo userAuthRepo
            , IOptions<SOSAccessTokenCredentialsDTO> SOSAccessTokenCredentialsDTO
            , IUserTokenRepo userTokenRepo)
        {
            _SOSAppCredentialsDTO = SOSAppCredentialsDTO;
            _userAuthRepo = userAuthRepo;
            _SOSAccessTokenCredentialsDTO = SOSAccessTokenCredentialsDTO;
            _userTokenRepo = userTokenRepo;
        }

        public ResponseDTO<AuthDetailsDTO> AddSOSAuthCode(string code)
        {
            AuthDetailsDTO authDetailsDTO = new AuthDetailsDTO()
            {
                Code = code,
                Type = CompanyTypeEnum.SOS,
            };
            var resp = _userAuthRepo.AddAuthDetail(authDetailsDTO);

            return resp;
        }

        public AuthCodeDTO GetLatestAuthCode()
        {
           return _userAuthRepo.GetLatestAuthCode(Common.Enums.CompanyTypeEnum.SOS);
        }

        public string GetAuthCodeUrl()
        {
            SOSAppCredentialsDTO urlItem = this.GetSOSAppCredentials();
            return urlItem.SOSBaseURI + urlItem.RoutesOAuth2 + urlItem.Authorize + urlItem.ResponseType + urlItem.ClientId + urlItem.RedirectUrl + urlItem.URLToRedirect;
        }

        public TokenDTO GenerateAccessTokenFromAuthorizationCode(string code)
        {

            SOSAccessTokenCredentialsDTO sOSAccessTokenCredentialsDTO = _SOSAccessTokenCredentialsDTO.Value; 
            //POST https://api.sosinventory.com/oauth2/token
            var client = new RestClient(new RestClientOptions(sOSAccessTokenCredentialsDTO.request));

            //POST https://api.sosinventory.com/oauth2/token
            var request = new RestRequest(sOSAccessTokenCredentialsDTO.request, Method.Post);

            //application/x-www-form-urlencoded
            request.AddHeader("Content-Type", sOSAccessTokenCredentialsDTO.content_Type);

            //Host  "api.sosinventory.com"
            request.AddHeader("Host", sOSAccessTokenCredentialsDTO.host);

            //Request body
            request.AddParameter("grant_type", sOSAccessTokenCredentialsDTO.grant_Type);
            request.AddParameter("client_id", sOSAccessTokenCredentialsDTO.client_id, false);
            request.AddParameter("client_secret", sOSAccessTokenCredentialsDTO.client_secret, false);
            request.AddParameter("code", code, false);
            request.AddParameter("redirect_uri", sOSAccessTokenCredentialsDTO.redirect_Url);

            //Contains access token
            var queryResult = client.Execute(request);
            TokenDTO tokenDTO = JsonConvert.DeserializeObject<TokenDTO>(queryResult.Content);

            //Return tokens
            return tokenDTO;
        }

        private SOSAppCredentialsDTO GetSOSAppCredentials()
        {
            SOSAppCredentialsDTO sOSAppCredentialsDTO = _SOSAppCredentialsDTO.Value;
            return new SOSAppCredentialsDTO
            {
                Authorize = sOSAppCredentialsDTO.Authorize,
                ClientId = sOSAppCredentialsDTO.ClientId,   
                ClientSecret = sOSAppCredentialsDTO.ClientSecret,
                RedirectUrl = sOSAppCredentialsDTO.RedirectUrl,
                ResponseType = sOSAppCredentialsDTO.ResponseType,
                RoutesOAuth2 = sOSAppCredentialsDTO.RoutesOAuth2,
                SOSBaseURI = sOSAppCredentialsDTO.SOSBaseURI,
                URLToRedirect = sOSAppCredentialsDTO.URLToRedirect
            };
        }

        public ResponseDTO<TokenDTO> AddTokens(TokenDTO tokenDTO, Guid AccountId)
        {
            AddTokenDTO addTokenDTO = new AddTokenDTO
            {
                AccountId = AccountId,
                AccessToken = tokenDTO.access_token,
                AccessTokenExpireIn = tokenDTO.expires_in,
                RefreshToken = tokenDTO.refresh_token,
                Type = tokenDTO.token_type
            };

             _userTokenRepo.AddToken(addTokenDTO);
            return new ResponseDTO<TokenDTO> { Data = tokenDTO, Message = "Added Successfully", Status = 200 };

        }
    }
}
