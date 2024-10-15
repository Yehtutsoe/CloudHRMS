using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories;

namespace CloudHRMS.Services
{
	public class DepartmentService : IDepartmentService
	{
		private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            this._departmentRepository = departmentRepository;
        }
        public void Create(DepartmentViewModel departmentViewModel)
		{
			_departmentRepository.Create(departmentViewModel);
		}

		public void Delete(string Id)
		{
			_departmentRepository.Delete(Id);
		}

		public DepartmentViewModel GetById(string Id)
		{
			return _departmentRepository.GetById(Id);
		}

		public IList<DepartmentViewModel> RetireveAll()
		{
			return _departmentRepository.RetireveAll();
		}

		public void Update(DepartmentViewModel departmentViewModel)
		{
			_departmentRepository.Update(departmentViewModel);
		}
	}
}
