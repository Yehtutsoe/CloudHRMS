using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility.Network;
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
		public IActionResult Entry(DepartmentViewModel departmentViewModel)
		{
			var isAlreadyExist = _applicationDbContext.Departments.Where(w => w.Code == departmentViewModel.Code).Any();
			if (isAlreadyExist)
			{
				error.Message = $"This code {isAlreadyExist} is already exist in the system try another";
				return View();
			}
			try
			{
				DepartmentEntity departmentEntity = new DepartmentEntity()
				{
					Id = Guid.NewGuid().ToString(),
					Code = departmentViewModel.Code,
					Description = departmentViewModel.Description,
					ExtensionPhone = departmentViewModel.ExtensionPhone,
					IsActive = true,
					IpAddress = NetworkHelper.GetMechinePublicIP(),
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
				var existingDepartmentEntity = _applicationDbContext.Departments.Find(departmentViewModel.Id);
				existingDepartmentEntity.Code = departmentViewModel.Code;
				existingDepartmentEntity.Description = departmentViewModel.Description;
				existingDepartmentEntity.ExtensionPhone = departmentViewModel.ExtensionPhone;
				existingDepartmentEntity.CreatedBy = "system";
				existingDepartmentEntity.CreatedAt = DateTime.Now;
				existingDepartmentEntity.IsActive = true;
				existingDepartmentEntity.IpAddress = NetworkHelper.GetMechinePublicIP();
				_applicationDbContext.Departments.Update(existingDepartmentEntity);
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
		
	}
}
