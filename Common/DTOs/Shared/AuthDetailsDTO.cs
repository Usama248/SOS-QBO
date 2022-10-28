using Common.Enums;

namespace Common.DTOs.Shared
{
    public class AuthDetailsDTO
    {
        public Guid? Id { get; set; }
        public string? CompanyName { get; set; }
        public string Code { get; set; }
        public bool IsConnected { get; set; }
        public CompanyTypeEnum Type { get; set; }
        public DateTime? LastSynced { get; set; }
    }
}
