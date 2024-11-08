using CloudHRMS.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.ViewModels
{
    public class AttendanceMasterViewModel
    {
        public string Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime ToDate { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }
        public bool IsLate { get; set; }
        public bool IsEarlyOut { get; set; }
        public bool IsLeave { get; set; }
        public string EmployeeId { get; set; } //CRUD Process

        public string DepartmentId { get; set; } //CRUD Process
      
        public string ShiftId { get; set; } //CRUD Process

        public IList<DepartmentViewModel> DepartmentViewModels { get; set; } //Bind for UI
        public IList<EmployeeViewModel> EmployeeViewModels { get; set; } // Bind for UI
        public IList<ShiftViewModel> ShiftViewModels { get; set; } // Bind for UI
    }
}
