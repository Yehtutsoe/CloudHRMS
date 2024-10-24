using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Repositories
{
	public interface IEmployeeRepository
	{
		Task Create(EmployeeViewModel employeeViewModel);
		EmployeeViewModel GetById(string Id);
		IList<DepartmentViewModel> GetActiveDepartment();
		IList<PositionViewModel> GetActivePosition();
		IList<EmployeeViewModel> RetireveAll();
		void Update(EmployeeViewModel employeeViewModel);
		void Delete(string Id);
	}
}
