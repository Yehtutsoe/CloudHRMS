using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CloudHRMS.Models.Entities
{
    [Table("Shift")]
    public class ShiftEntity:BaseEntity
    {
        public  string Id { get; set; }
        [MaxLength(35)]
        public string Name { get; set; }
        [MaxLength(10)]
        public TimeSpan InTime { get; set; }
        [MaxLength(10)]
        public TimeSpan OutTime { get; set; }
        [MaxLength(10)]
        public TimeSpan LateAfter { get; set; }
        [MaxLength(10)]
        public TimeSpan EarlyOutBefore { get; set; }
        public string AttendancePolicyId { get; set; }
        [ForeignKey(nameof(AttendancePolicyId))]
        public AttendancePolicyEntity AttendancePolicy { get; set; }
    }
}
