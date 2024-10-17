using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
	public class AttendancePolicyController : Controller
	{
		private readonly IAttendancePolicyService _attendancePolicyService;

		ErrorViewModel error = new ErrorViewModel();
        public AttendancePolicyController(IAttendancePolicyService attendancePolicyService)
        {
            _attendancePolicyService = attendancePolicyService;
        }
        public IActionResult Entry()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Entry(AttendancePolicyViewModel attendancePolicyViewModel)
		{
			try
			{
				_attendancePolicyService.Create(attendancePolicyViewModel);
				error.Message = "Successful Save to system";
				error.IsOccurError = false;
			}
			catch (Exception e)
			{
				error.Message = "Error Occour to save";
				error.IsOccurError = true;
				throw e;
			}
			return View();
		}
	}
}
