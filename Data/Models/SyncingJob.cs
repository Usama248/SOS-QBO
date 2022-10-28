using Common.Enums.QB;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class SyncingJob : BaseEntity
    {
        public SyncingJob()
        {
            SyncingJobEntities = new HashSet<SyncingJobEntity>();
        }

        [ForeignKey("QBAccount")]
        public Guid QBAccountId { get; set; }

        [ForeignKey("SoSAccount")]
        public Guid SoSAccountId { get; set; }
        public string Message { get; set; }
        public int JobNumber { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public JobStatusEnum? Status { get; set; }
        public SyncingTypeEnum? SyncingType { get; set; }
        public bool InUse { get; set; }

        public virtual Account QBAccount { get; set; }
        public virtual Account SoSAccount { get; set; }
        public ICollection<SyncingJobEntity> SyncingJobEntities { get; set; }
    }
}
