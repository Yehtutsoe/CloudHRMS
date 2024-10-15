using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories;

namespace CloudHRMS.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
			this._employeeRepository = employeeRepository;
        }
		public void Create(EmployeeViewModel employeeView)
		{
			_employeeRepository.Create(employeeView);
		}

		public void Delete(string Id)
		{
			_employeeRepository.Delete(Id);
		}

		public EmployeeViewModel GetById(string id)
		{
			return _employeeRepository.GetById(id);
		}

		public EmployeeViewModel PrepareEntryForm()
		{
			var employeeViewModel = new EmployeeViewModel
			{
				DepartmentsViewModel = _employeeRepository.GetActiveDepartment(),
				PositionsViewModel = _employeeRepository.GetActivePosition()
			};
			return employeeViewModel;
		}

		public IList<EmployeeViewModel> ReterieveAll()
		{
			return _employeeRepository.RetireveAll();
		}

		public void Update(EmployeeViewModel employeeView)
		{
			_employeeRepository.Update(employeeView);
		}
	}
}
