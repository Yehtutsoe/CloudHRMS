using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.Entities
{
	[Table("DailyAttendance")]
	public class DailyAttendanceEntity:BaseEntity
    {
        [MaxLength(15)]
        public DateTime AttendanceDate { get; set; }
        [MaxLength(15)]
        public TimeSpan InTime { get; set; }
		[MaxLength(15)]
		public TimeSpan OutTime { get; set; }
        public string EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public EmployeeEntity Employee { get; set; }
        public string DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public DepartmentEntity Department { get; set; }

    }
}
