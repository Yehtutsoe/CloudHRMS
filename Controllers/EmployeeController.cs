using Microsoft.AspNetCore.Mvc;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using CloudHRMS.DAO;

namespace CloudHRMS.Controllers
{

	public class EmployeeController : Controller
	{
		private readonly IEmployeeService _employeeService;
		
		ErrorViewModel error = new ErrorViewModel();

		public EmployeeController(IEmployeeService employeeService)
		{
			_employeeService = employeeService;

		}
		public IActionResult Entry() {
			return View(_employeeService.PrepareEntryForm());
		}

		[HttpPost]
		public IActionResult Entry(EmployeeViewModel employeeViewModel)
		{
			try {
				_employeeService.Create(employeeViewModel);
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
		public IActionResult List()=>View(_employeeService.ReterieveAll());

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

		public IActionResult Edit(string Id)=> View(_employeeService.GetById(Id));
		

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

	}
}
