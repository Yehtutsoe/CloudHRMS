using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.Entities
{
    [Table("AttendancePolicy")]
	public class AttendancePolicyEntity:BaseEntity
	{
        public string Name { get; set; }
        public int NumberOfLateTimes { get; set; }
        public int NumberOfEarlyOutTimes { get; set; }
        public Decimal DeductionInAmount { get; set; }
        public int DeductionInDay { get; set; }
    }
}
