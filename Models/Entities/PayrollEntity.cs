using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.Entities
{
    [Table("Payroll")]
    public class PayrollEntity:BaseEntity
    {
        public string Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Decimal IncomeTax { get; set; }
        public Decimal GrossPay { get; set; }
        public Decimal NetPay   { get; set; }
        public Decimal Allowance { get; set; }
        public Decimal Deduction { get; set; }
        public Decimal AttendanceDays { get; set; }
        public Decimal AttendanceDeductions { get; set; }
        public string EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public EmployeeEntity Employees { get; set; }
        public string DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public DepartmentEntity Departments { get; set; }
    }
}
