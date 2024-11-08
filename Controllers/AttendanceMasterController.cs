using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class AttendanceMasterController : Controller
    {
        private readonly IAttendanceMasterService _attendanceMasterService;

        public AttendanceMasterController(IAttendanceMasterService attendanceMasterService)
        {
            _attendanceMasterService = attendanceMasterService;
        }
        public IActionResult DayEndProcess()
        {
            
            return View(_attendanceMasterService.PreparedFrom());
        }
        [HttpPost]
        public IActionResult DayEndProcess(AttendanceMasterViewModel attendanceMasterViewModel) {
            _attendanceMasterService.DayEndProcess(attendanceMasterViewModel);
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
           var attendanceMaster= _attendanceMasterService.RetrieveAll();
            return View(attendanceMaster);
        }
    }
}
