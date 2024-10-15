using CloudHRMS.Controllers;
using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
	public interface IEmployeeService
	{
		EmployeeViewModel PrepareEntryForm();
		void Create(EmployeeViewModel employeeView);
		EmployeeViewModel GetById(string id);
		IList<EmployeeViewModel> ReterieveAll();
		void Update(EmployeeViewModel employeeView);
		void Delete(string id);
		
	}
}
