﻿using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IDepartmentService _departmentService;
		ErrorViewModel error = new ErrorViewModel();

		public DepartmentController(IDepartmentService departmentService)
		{
			this._departmentService = departmentService;
		}

		public IActionResult Entry()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Entry(DepartmentViewModel departmentViewModel)
		{
			try
			{
				_departmentService.Create(departmentViewModel);
				error.Message = "Transaction save successful to the System";
			}
			catch
			{
				error.Message = "Transaction not save,found error";
				error.IsOccurError = true;
			}
			ViewBag.Msg = error;

			return RedirectToAction("List");
		}


		public IActionResult List() => View(_departmentService.RetireveAll());

		public IActionResult Edit(string Id)=>View(_departmentService.GetById(Id));

		[HttpPost]
		public IActionResult Update(DepartmentViewModel departmentViewModel)
		{
			try
			{
				_departmentService.Update(departmentViewModel);
				TempData["Msg"] = "Successful update to the system";
				TempData["IsOccourError"] = false;
			}
			catch
			{
				TempData["Msg"] = "Error Occour";
				TempData["IsOccourError"] = true;

			}

			return RedirectToAction("List");
		}

		public IActionResult Delete(string Id)
		{
			try
			{
				_departmentService.Delete(Id);
				TempData["Msg"] = "Successful Delete from the system";
				TempData["IsOccourError"] = false;
			}
			catch
			{
				TempData["Msg"] = "Error Occour";
				TempData["IsOccourError"] = true;
			}
			return RedirectToAction("List");
		}
	}
}


		

