using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class DailyAttendanceController : Controller
    {
        public IActionResult Entry()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }
    }
}
