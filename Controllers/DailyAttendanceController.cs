using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CloudHRMS.Controllers
{
    public class DailyAttendanceController : Controller
    {
        private readonly IDailyAttendanceService _dailyAttendanceService;

		#region Constructor
		public DailyAttendanceController(IDailyAttendanceService dailyAttendanceService)
        {
            _dailyAttendanceService = dailyAttendanceService;
        }
		#endregion

		[Authorize(Roles = "HR")]
		public IActionResult Entry()
        {
            return View(_dailyAttendanceService.PreparedEntryForm());
        }
		#region Create
		[Authorize(Roles ="HR")]
        [HttpPost]
        public IActionResult Entry(DailyAttendanceViewModel dailyAttendanceViewModel)
        {
            try
            {
                _dailyAttendanceService.Create(dailyAttendanceViewModel);
                TempData["Msg"] = "Successfully Save Record to System";
                TempData["IsOccourError"] = false;
            }
            catch (Exception e)
            {
				TempData["Msg"] = "Error Occour ";
				TempData["IsOccourError"] = true;
				throw e;
            }
            return RedirectToAction("List");
        }
		#endregion

		#region Retrieve
		public IActionResult List()
		{
			return View(_dailyAttendanceService.ReterieveAll());
		}
		#endregion

		#region Edit
		[Authorize(Roles = "HR")]
		public IActionResult Edit(string Id)
        {
            return View(_dailyAttendanceService.GetById(Id));
        }
		#endregion

		#region Update
		[Authorize(Roles ="HR")]
        [HttpPost]
        public IActionResult Update(DailyAttendanceViewModel dailyAttendanceViewModel)
        {

            try
            {
                _dailyAttendanceService.Update(dailyAttendanceViewModel);
                TempData["Msg"] = "Successfully Update the Record to the System";
                TempData["IsOccourError"] = false;
            }
            catch (Exception e)
            {
				TempData["Msg"] = "Error Occour ";
				TempData["IsOccourError"] = true;
				throw e;
            }
            return RedirectToAction("List");
        }
		#endregion

		#region Delete
		[Authorize(Roles ="HR")]
        public IActionResult Delete(string Id) {
			try
			{
				_dailyAttendanceService.Delete(Id);
				TempData["Msg"] = "Successfully Delete the record from the system";
				TempData["IsOccourError"] = false;
			}
			catch
			{
				TempData["Msg"] = "Error Occour ";
				TempData["IsOccourError"] = true;
			}
			
            return RedirectToAction("List");
        }
		#endregion
	}
}
