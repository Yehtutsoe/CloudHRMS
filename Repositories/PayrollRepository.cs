using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PayrollRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void AddPayroll(IEnumerable<PayrollEntity> payrolls)
        {
            _applicationDbContext.Payrolls.AddRange(payrolls);
            _applicationDbContext.SaveChanges();
        }

        public IList<DepartmentViewModel> GetActiveDepartments()
        {
            return _applicationDbContext.Departments.Where(w => w.IsActive).Select(s => new DepartmentViewModel

            {
                Id = s.Id,
                Code = s.Code + "|" + s.Description
            }).ToList();
        }

        public IList<EmployeeViewModel> GetActiveEmployees()
        {
            return _applicationDbContext.Employees.Where(w => w.IsActive).Select(s => new EmployeeViewModel
            {
                Id = s.Id,
                FullName = s.No + "|" + s.FullName
            }).ToList();
        }
        #region GetDepartment
        public IEnumerable<DepartmentEntity> GetAllDepartments()
        {
            return _applicationDbContext.Departments.ToList();
        }
        #endregion

        #region GetEmployees
        public IEnumerable<EmployeeEntity> GetAllEmployees()
        {
            return _applicationDbContext.Employees.ToList();
        }
        #endregion

        public IEnumerable<PayrollEntity> GetAllPayrolls()
        {
            return _applicationDbContext.Payrolls.ToList();
        }

        public PayrollEntity GetPayrollById(int id)
        {
           return _applicationDbContext.Payrolls.Find(id);
        }
    }
}
