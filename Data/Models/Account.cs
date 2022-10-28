
using Common.Enums;

namespace Data.Models
{
    public class Account : BaseEntity
    {
        public Account()
        {
            AuthTokens = new HashSet<AuthToken>();
        }
        public string? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string Code { get; set; }
        public bool IsConnected { get; set; }
        public CompanyTypeEnum Type { get; set; }
        public DateTime? LastSynced { get; set; }
        public virtual ICollection<AuthToken> AuthTokens { get; set; }
    }
}
