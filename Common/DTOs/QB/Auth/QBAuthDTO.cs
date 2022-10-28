using Common.Enums;

namespace Common.DTOs.QB.Auth
{
    public class QBAuthDTO
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectURL { get; set; }
        public string Environment { get; set; }
        public string QBBaseURL { get; set; }
    }

    public class QBTokenPropertiesDTO
    {
        public string AccessToken { get; set; }
        public string RealmId { get; set; }
        public string RefreshToken { get; set; }
    }
    public class QBAddAuthCodeDTO
    {
        public string AuthCode { get; set; }
        public string RealMId { get; set; }
        public CompanyTypeEnum Type { get; set; }
    }
}
