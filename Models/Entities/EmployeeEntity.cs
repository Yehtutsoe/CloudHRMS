using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.Entities
{
    [Table("Employee")]
	public class EmployeeEntity:BaseEntity
	{
       
        public string No { get; set; }
        public string FullName { get; set; }
        [MaxLength(6)]
        public string Gender { get; set; }
        [MaxLength(12)]
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        [Precision(18,2)]
        public decimal Salary { get; set; }
        public DateTime? DOE { get; set; } //Date of employee
        public DateTime? DOR { get; set; } // Date of retire
        public string Address { get; set; }

        public string DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public DepartmentEntity Department { get; set; }

        public string PositionId { get; set; }
        [ForeignKey(nameof(PositionId))]
        public PositionEntity Position { get; set; }

        public string UserId { get; set; }  // for authentication login 

    }
}
