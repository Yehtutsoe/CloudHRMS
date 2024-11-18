using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
    public interface IPayrollService
    {
        List<PayrollViewModel> GetPayrollList();
        List<PayrollEntity> ProcessPayroll(PayrollProcessViewModel payrollProcessViewModel);
        PayrollViewModel PreparyEntry();
    }
}
