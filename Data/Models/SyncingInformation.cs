using Common.Enums.QB;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class SyncingInformation : BaseEntity
    {
        public Guid QBAccountId { get; set; }
        public Guid SoSAccountId { get; set; }
        public string SoSEntityId { get; set; }
        public string QBEntityId { get; set; }
        public string Message { get; set; }
        public EntityTypeEnum? EntityType { get; set; }
        public SyncingStatusEnum? Status { get; set; }
        public SyncingTypeEnum? SyncingType { get; set; }
        public bool? FromQuickBooks { get; set; }
        public bool? IsIgnored { get; set; }
        public bool? AnotherMatchFound { get; set; }
        public string MatchJson { get; set; }
        public string EditSequence { get; set; }
        public DateTime? LastSyncDate { get; set; }

        [ForeignKey("SyncingJob")]
        public Guid? SyncingJobId { get; set; }
        public virtual SyncingJob SyncingJob { get; set; }
    }
}
