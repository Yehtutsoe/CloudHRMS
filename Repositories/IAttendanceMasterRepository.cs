using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Repositories
{
	public interface IAttendanceMasterRepository
	{
		void DayEndProcess(AttendanceMasterViewModel attendanceMasterViewModel);
		IList<AttendanceMasterViewModel> RetrieveAll();
		IList<DepartmentViewModel> GetActiveDeparment();
		IList<EmployeeViewModel> GetActiveEmployees();
		IList<ShiftViewModel> GetActiveShift();
	}
}
