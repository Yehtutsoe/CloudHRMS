using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
	public interface IDepartmentService
	{
		void Create(DepartmentViewModel departmentViewModel);
		DepartmentViewModel GetById(string Id);
		IList<DepartmentViewModel> RetireveAll();
		void Update(DepartmentViewModel departmentViewModel);
		void Delete(string Id);
	}
}
