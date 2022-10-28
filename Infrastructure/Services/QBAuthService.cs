using Application.IRepos;
using Application.IServices;
using Common.DTOs;
using Common.DTOs.QB.Auth;
using Common.DTOs.Shared;
using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    public class QBAuthService : IQBAuthService
    {
        private readonly QBAuthDTO _QBAuthDTO;
        private readonly IQBAuthRepo _qBAuthRepository;
        private readonly IQBTokenRepo _qBTokenRepo;

        public QBAuthService(IOptions<QBAuthDTO> QBAuthDTO, IQBAuthRepo qBAuthRepository, IQBTokenRepo qBTokenRepo)
        {
            _qBAuthRepository = qBAuthRepository;
            _QBAuthDTO = new Common.DTOs.QB.Auth.QBAuthDTO
            {
                ClientId = QBAuthDTO.Value.ClientId,
                ClientSecret = QBAuthDTO.Value.ClientSecret,
                Environment = QBAuthDTO.Value.Environment,
                RedirectURL = QBAuthDTO.Value.RedirectURL,
            };
            _qBTokenRepo = qBTokenRepo;
        }

        public ResponseDTO<AuthDetailsDTO> AddQBAuthCode(QBAddAuthCodeDTO qBAddAuthCodeDTO)
        {
            _qBAuthRepository.Add(qBAddAuthCodeDTO);
            var data = _qBAuthRepository.GetLatestAuthDetails();
            AuthDetailsDTO authDetailsDTO = new AuthDetailsDTO
            {
                Code = data.Code,
                CompanyName = data.CompanyName,
                Id = data.Id,
            };

            return new ResponseDTO<AuthDetailsDTO> { Data = authDetailsDTO, Message = "Added Successfully", Status = 200 };
        }

        public async Task GetAuthTokensAsync(string code, string realmId)
        {

            OAuth2Client auth2Client = new OAuth2Client(
              _QBAuthDTO.ClientId, _QBAuthDTO.ClientSecret,
              _QBAuthDTO.RedirectURL, _QBAuthDTO.Environment);

            var tokenResponse = await auth2Client.GetBearerTokenAsync(code);


            var access_token = tokenResponse.AccessToken;
            var access_token_expires_at = tokenResponse.AccessTokenExpiresIn;

            var refresh_token = tokenResponse.RefreshToken;
            var refresh_token_expires_at = tokenResponse.RefreshTokenExpiresIn;

            AddTokenDTO qBToken = new AddTokenDTO()
            {
                AccessToken = access_token,
                RefreshToken = refresh_token,
                AccessTokenExpireIn = access_token_expires_at.ToString(),
                RefereshTokenExpiresIn = refresh_token_expires_at.ToString(),
            };

            var resp = this.AddToken(qBToken); 
        }

        public bool AddToken(AddTokenDTO addTokenDTO)
        {
           return _qBTokenRepo.AddQbToken(addTokenDTO);
        }

        public string GetAuthorizationURL()
        {
            //Set the cridentials for call.
            OAuth2Client auth2Client = new OAuth2Client(
               _QBAuthDTO.ClientId, _QBAuthDTO.ClientSecret,
               _QBAuthDTO.RedirectURL, _QBAuthDTO.Environment);

            //Some QB pre written code.
            List<OidcScopes> scopes = new List<OidcScopes>();
            scopes.Add(OidcScopes.Accounting);
            try
            {
                string authorizeUrl = auth2Client.GetAuthorizationURL(scopes);
                return authorizeUrl;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public TokenDTO GetLatestQbAccessToken()
        {
            return _qBTokenRepo.GetLatestQbToken();
        }
    }
}
