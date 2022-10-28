namespace Common.DTOs.Shared
{
    public class AddTokenDTO
    {
        public Guid AccountId { get; set; }
        public string AccessToken { get; set; }
        public string Type { get; set; }
        public string AccessTokenExpireIn { get; set; }
        public string RefreshToken { get; set; }
        public string RefereshTokenExpiresIn { get; set; }
    }
}
