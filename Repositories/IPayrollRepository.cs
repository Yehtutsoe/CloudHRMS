using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Repositories
{
    public interface IPayrollRepository
    {
        IEnumerable<PayrollEntity> GetAllPayrolls();
        void AddPayroll(IEnumerable<PayrollEntity> payrolls);
        PayrollEntity GetPayrollById(int id);
        IEnumerable<EmployeeEntity> GetAllEmployees();
        IEnumerable<DepartmentEntity> GetAllDepartments();
        IList<EmployeeViewModel> GetActiveEmployees();
        IList<DepartmentViewModel> GetActiveDepartments();

    }
}
