using Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class AuthToken : BaseEntity
    {
        public CompanyTypeEnum CompanyTypeEnum { get; set; }
        public string AccessToken { get; set; }
        public string Type { get; set; }
        public long ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public bool IsExpired { get; set; }

        [ForeignKey("Account")]
        public Guid? CompanyId { get; set; }
        public Account Account { get; set; }

    }
}
