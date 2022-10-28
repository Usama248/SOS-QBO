using Common.Enums;

namespace Common.DTOs.Shared
{
    public class TokenDTO
    {
        public Guid? Id { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public long expires_in { get; set; }
        public string refresh_token { get; set; }
        public CompanyTypeEnum company_type { get; set; }
    }
}
