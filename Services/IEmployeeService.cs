
using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
	public interface IEmployeeService
	{
		EmployeeViewModel PrepareEntryForm();
		Task Create(EmployeeViewModel employeeView);
		EmployeeViewModel GetById(string id);
		IList<EmployeeViewModel> ReterieveAll();
		void Update(EmployeeViewModel employeeView);
		void Delete(string id);
		
	}
}
