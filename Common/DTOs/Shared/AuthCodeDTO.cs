using Common.Enums;

namespace Common.DTOs.Shared
{
    public class AuthCodeDTO
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string? CompanyName { get; set; }
        public CompanyTypeEnum Type { get; set; }
    }
}
