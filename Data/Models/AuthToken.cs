using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class AuthToken : BaseEntity
    {

        [ForeignKey("Account")]
        public Guid AccountId { get; set; }
        public string AccessToken { get; set; }
        public string Type { get; set; }
        public string ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public bool IsExpired { get; set; }
        public virtual Account Account { get; set; }
    }
}
