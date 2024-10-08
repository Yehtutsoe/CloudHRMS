using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CloudHRMS.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly ApplicationDbContext _applicationDbContext;

		ErrorViewModel error = new ErrorViewModel();

        public DepartmentController(ApplicationDbContext applicationDbContext)
        {
			_applicationDbContext = applicationDbContext;
        }

        public IActionResult DepartmentEntry()
		{
			return View();
		}

		[HttpPost]
		public IActionResult DepartmentEntry(DepartmentViewModel departmentViewModel)
		{

			try
			{
				DepartmentEntity departmentEntity = new DepartmentEntity()
				{
					Id = Guid.NewGuid().ToString(),
					Code = departmentViewModel.Code,
					Description = departmentViewModel.Description,
					ExtensionPhone = departmentViewModel.ExtensionPhone,
					IsActive = true,
					IpAddress = GetIpAddressofMachine(),
					CreatedAt = DateTime.Now,
					CreatedBy = "System"

				};
				_applicationDbContext.Departments.Add(departmentEntity);
				_applicationDbContext.SaveChanges();
				error.Message = "Transaction save successful to the System";
			}
			catch
			{

				error.Message = "Transaction not save,found error";
				error.IsOccurError = true;
			}
			ViewBag.Msg = error;

			return View();
		}

		public IActionResult List()
		{
			IList<DepartmentViewModel> departments = _applicationDbContext.Departments
														.Where(w => w.IsActive)
														.Select(s => new DepartmentViewModel
														{
															Id = s.Id,
															Code = s.Code,
															Description = s.Description,
															ExtensionPhone = s.ExtensionPhone,
															

														}).ToList();
			return View(departments);
		}

		public IActionResult Edit(string Id)
		{
			var department = _applicationDbContext.Departments.Where(w => w.Id == Id).Select(s => new DepartmentViewModel
			{
				Id = s.Id,
				Code = s.Code,
				Description = s.Description,
				ExtensionPhone = s.ExtensionPhone

			}).SingleOrDefault();
			return View(department);
		}
		[HttpPost]
		public IActionResult Update(DepartmentViewModel departmentViewModel)
		{
			try
			{
				DepartmentEntity departments = new DepartmentEntity()
				{
					Id = departmentViewModel.Id,
					Code = departmentViewModel.Code,
					Description = departmentViewModel.Description,
					ExtensionPhone = departmentViewModel.ExtensionPhone,
					CreatedBy = "system",
					CreatedAt = DateTime.Now,
					IsActive = true,
					IpAddress = GetIpAddressofMachine()

				};
				_applicationDbContext.Departments.Update(departments);
				_applicationDbContext.SaveChanges();
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
				var existingDepartments = _applicationDbContext.Departments.Where(w => w.Id == Id).SingleOrDefault();
				if (existingDepartments is not null)
				{
					existingDepartments.IsActive = false;
					_applicationDbContext.Update(existingDepartments);
					_applicationDbContext.SaveChanges();
					TempData["Msg"] = "Successful Delete from the system";
					TempData["IsOccourError"] = false;
				}
			}
			catch 
			{
				TempData["Msg"] = "Error Occour";
				TempData["IsOccourError"] = true;
			}
			return RedirectToAction("List");
		}
		public string GetIpAddressofMachine()
		{
			return HttpContext.Connection.RemoteIpAddress.ToString();
		}
	}
}
