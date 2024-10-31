using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Repositories
{
	public interface IDailyAttendanceRepository
	{
		IList<DepartmentViewModel> GetActiveDepartment();
		IList<EmployeeViewModel> GetActiveEmployee();
		void Create(DailyAttendanceViewModel dailyAttendanceView);
		IList<DailyAttendanceViewModel> ReterieveAll();
		DailyAttendanceViewModel GetById(string Id);
		void Update(DailyAttendanceViewModel dailyAttendanceView);
		void Delete(string Id);
	}
}
