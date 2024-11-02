using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.Entities
{
    public class Shift:BaseEntity
    {
        public string Name { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }
        public TimeSpan LateAfter { get; set; }
        public TimeSpan EarlyOutBefore { get; set; }
        public string AttendancePolicyId { get; set; }
        [ForeignKey(nameof(AttendancePolicyId))]
        public AttendancePolicyEntity AttendancePolicy { get; set; }
    }
}
