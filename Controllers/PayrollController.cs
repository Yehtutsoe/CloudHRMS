using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class PayrollController : Controller
    {
        private readonly IPayrollService _payrollService;

        public PayrollController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }
        public IActionResult List()
        {
            return View();
        }
        public IActionResult PayrollProcess()
        {
            return View(_payrollService.PreparyEntry());
        }
        [HttpPost]
        [Authorize(Roles ="HR")]
        public IActionResult PayrollProcess(PayrollProcessViewModel payrollProcessViewModel)
        {
            return View(payrollProcessViewModel);
        }
    }
}
