using Common.Enums.QB;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class SyncingJobEntity : BaseEntity
    {
        public SyncingDirectionEnum? Direction { get; set; }
        public SyncingTypeEnum? SyncingType { get; set; }
        public EntityStatusEnum? Status { get; set; }
        public EntityTypeEnum? EntityType { get; set; }
        public string Message { get; set; }
        public string ExceptionDetails { get; set; }

        [ForeignKey("SyncingJob")]
        public Guid? SyncingJobId { get; set; }
        public virtual SyncingJob? SyncingJob { get; set; }
        public ICollection<SyncingLog> AccountingServiceSyncingLogs { get; set; }
    }
}
