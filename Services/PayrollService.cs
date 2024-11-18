using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories;

namespace CloudHRMS.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IPayrollRepository _payrollRepository;

        public PayrollService(IPayrollRepository payrollRepository)
        {
            _payrollRepository = payrollRepository;
        }
        public List<PayrollViewModel> GetPayrollList()
        {
            var payrolls = _payrollRepository.GetAllPayrolls();
            var departments = _payrollRepository.GetAllDepartments();
            var employees = _payrollRepository.GetAllEmployees();

            var payrollList = (from p in payrolls
                               join e in employees on p.EmployeeId equals e.Id
                               join d in departments on p.DepartmentId equals d.Id
                               select new PayrollViewModel { 
                                   Id = p.Id,
                                   FromDate = p.FromDate,
                                   ToDate = p.ToDate,
                                   EmployeeInfo = e.No + "|" + e.FullName,
                                   DepartmentInfo = d.Description,
                                   BasicSalary = e.Salary,
                                   GrossPay = p.GrossPay,
                                   AttendanceDays = p.AttendanceDays,
                                   AttendanceDeduction = p.AttendanceDeductions,
                                   Allowance = p.Allowance,
                                   IncomeTax = p.IncomeTax,
                                   NetPay = p.NetPay

                               }).ToList();
            return payrollList;
        }

        public PayrollViewModel PreparyEntry()
        {
            var payrollViewModel = new PayrollViewModel()
            {
                DepartmentViewModels = _payrollRepository.GetActiveDepartments(),
                EmployeeViewModels = _payrollRepository.GetActiveEmployees()
            };
            return payrollViewModel;
        }

        public List<PayrollEntity> ProcessPayroll(PayrollProcessViewModel payrollProcessViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
