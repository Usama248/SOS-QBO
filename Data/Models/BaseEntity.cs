namespace Data.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreatedDateUTC { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDateUTC { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
