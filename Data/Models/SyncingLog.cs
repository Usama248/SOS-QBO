using Common.Enums.QB;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class SyncingLog : BaseEntity
    {
        public Guid QBAccountId { get; set; }
        public Guid SoSAccountId { get; set; }
        public string SoSEntityId { get; set; }
        public string QBEntityId { get; set; }
        public string Message { get; set; }
        public EntityTypeEnum? EntityType { get; set; }
        public SyncingStatusEnum? Status { get; set; }
        public SyncingTypeEnum? SyncingType { get; set; }
        public SyncingDirectionEnum? Direction { get; set; }
        public bool? AnotherMatchFound { get; set; }
        public string MatchJson { get; set; }
        public string AccountingEntity { get; set; }
        public string Description { get; set; }
        public string RequestId { get; set; }
        public string ExceptionDetails { get; set; }
        public string ExceptionStackTrace { get; set; }

        [ForeignKey("SyncingJobEntity")]
        public Guid? SyncingJobEntityId { get; set; }
        public virtual SyncingJobEntity SyncingJobEntity { get; set; }
    }
}
