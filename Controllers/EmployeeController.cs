using Microsoft.AspNetCore.Mvc;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Authorization;

namespace CloudHRMS.Controllers
{

	public class EmployeeController : Controller
	{
		private readonly IEmployeeService _employeeService;
		
		ErrorViewModel error = new ErrorViewModel();
		#region Constructor
		public EmployeeController(IEmployeeService employeeService)
		{
			_employeeService = employeeService;

		}
		#endregion

		//[Authorize(Roles = "HR")]
        public IActionResult Entry() {
			return View(_employeeService.PrepareEntryForm());
		}

		#region Create
		//[Authorize(Roles ="HR")]
		[HttpPost]
		public async Task<IActionResult> Entry(EmployeeViewModel employeeViewModel)
		{
			try {
				await _employeeService.Create(employeeViewModel);
				error.Message = "Successfully Save the record to the system";
			}
			catch
			{
				error.Message = "Oh,Error occur when Saving the record to the system";
				error.IsOccurError = true;
			}
			ViewBag.Msg = error;
			return RedirectToAction("List");
		}
		#endregion

		#region Retrieve
		public IActionResult List()
		{
			var employees = _employeeService.ReterieveAll();
			return View(employees);
		}
		#endregion

		#region Delete
		[Authorize(Roles = "HR")]
        //port://host/employee/delete?id=10
        public IActionResult Delete(string Id)
		{
			try { 
					_employeeService.Delete(Id);
					TempData["Msg"] = "Successful Deleted the record from the system";
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

		#region Edit
		[Authorize(Roles = "HR")]
        public IActionResult Edit(string Id)=> View(_employeeService.GetById(Id));
		#endregion

		#region Update
		[Authorize(Roles = "HR")]
        [HttpPost]
		public IActionResult Update(EmployeeViewModel employeeViewModel)
		{
			try { 
				_employeeService.Update(employeeViewModel);
				TempData["Msg"] = "Successful Updated the record to the system";
				TempData["IsOccourError"] = false;
			}
			catch(Exception)
			{
				TempData["Msg"] = "Error Occour ";
				TempData["IsOccourError"] = true;
			}
				return RedirectToAction("List");
		}
		#endregion

	}
}
