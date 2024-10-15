using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Repositories
{
	public interface IDepartmentRepository { 
		void Create(DepartmentViewModel departmentView);
		DepartmentViewModel GetById(string id);
		IList<DepartmentViewModel> RetireveAll();
		void Update(DepartmentViewModel departmentView);
		void Delete(string Id);
	}
}
