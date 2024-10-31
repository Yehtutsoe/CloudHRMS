using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

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
        [Authorize(Roles = "HR")]
        public IActionResult Entry()
		{
			return View();
		}
        [Authorize(Roles = "HR")]
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
			return RedirectToAction("List");
		}
		public IActionResult List() => View(_attendancePolicyService.RetrieveAll());
		
		public IActionResult Edit(string Id) => View(_attendancePolicyService.GetById(Id));
        [Authorize(Roles = "HR")]
        [HttpPost]
		public IActionResult Update(AttendancePolicyViewModel attendancePolicyViewModel)
		{
			_attendancePolicyService.Update(attendancePolicyViewModel);
			return RedirectToAction("List");
		}
        [Authorize(Roles = "HR")]
        public IActionResult Delete(string Id)
		{
			_attendancePolicyService.Delete(Id);
			return RedirectToAction("List");
		}
		
	}
}
