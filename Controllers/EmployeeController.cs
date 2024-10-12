using Microsoft.AspNetCore.Mvc;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Numerics;
using System.Reflection;
using CloudHRMS.Utility.Network;

namespace CloudHRMS.Controllers
{
	
	public class EmployeeController : Controller
	{
		private readonly ApplicationDbContext _applicationDbContext;

		ErrorViewModel error = new ErrorViewModel();

		public EmployeeController(ApplicationDbContext applicationDbContext	)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Entry()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Entry(EmployeeViewModel employeeViewModel)
		{
			
			try
			{
				EmployeeEntity employeeEntity = new EmployeeEntity()
				{
					Id = Guid.NewGuid().ToString(),
					No = employeeViewModel.No,
					FullName = employeeViewModel.FullName,
					Gender = employeeViewModel.Gender,
					DOB = employeeViewModel.DOB,
					DOE = employeeViewModel.DOE,
					Phone = employeeViewModel.Phone,
					Address = employeeViewModel.Address,
					Salary = employeeViewModel.Salary,
					Email = employeeViewModel.Email,
					DOR = employeeViewModel.DOR,
					CreatedAt = DateTime.Now,
					CreatedBy = "System",
					IsActive = true,
					IpAddress = GetIpAddressofMachine()
				};

				_applicationDbContext.Employees.Add(employeeEntity);
				_applicationDbContext.SaveChanges();
				error.Message = "Successfully Save the record to the system";
				
			}
			catch
			{

				error.Message = "Oh,Error occur when Saving the record to the system";
				error.IsOccurError = true;
			}
			ViewBag.Msg = error;
			return View();
		}

		public IActionResult List()
		{
			IList<EmployeeViewModel> employees =_applicationDbContext.Employees
														.Where(w=>w.IsActive)
														.Select(s => new EmployeeViewModel
														{
															Id=s.Id,
															No=s.No,
															FullName=s.FullName,
															Email =s.Email,
															Phone =s.Phone,
															Address = s.Address,
															Salary = s.Salary,
															Gender = s.Gender,
															DOB = s.DOB,
															DOE = s.DOE,
															DOR = s.DOR

														}).ToList();
			return View(employees);
		}

		//port://host/employee/delete?id=10
		public IActionResult Delete(string Id)
		{
			try
			{
				var existingEmployee = _applicationDbContext.Employees.Where(w => w.Id == Id).SingleOrDefault();
				if (existingEmployee is not null)
				{
					existingEmployee.IsActive = false;
					_applicationDbContext.Update(existingEmployee);
					_applicationDbContext.SaveChanges();
					TempData["Msg"] = "Successful Deleted the record from the system";
					TempData["IsOccourError"] = false;
				}
			}
			catch
			{

				TempData["Msg"] = "Error Occour ";
				TempData["IsOccourError"] = true;
			}
			return RedirectToAction("List");
		}

		public IActionResult Edit(string Id)
		{
			var employee = _applicationDbContext.Employees.Where(w => w.Id == Id).Select(s => new EmployeeViewModel
			{
				Id = s.Id,
				No = s.No,
				FullName = s.FullName,
				Email = s.Email,
				Phone = s.Phone,
				Address = s.Address,
				Salary = s.Salary,
				Gender = s.Gender,
				DOB = s.DOB,
				DOE = s.DOE,
				DOR = s.DOR
			}).SingleOrDefault();
			return View(employee);
		}

		[HttpPost]
		public IActionResult Update(EmployeeViewModel employeeViewModel)
		{
			try
			{
				var existingEmployeeEntity = _applicationDbContext.Employees.Find(employeeViewModel.Id);

				existingEmployeeEntity.No = employeeViewModel.No;
				existingEmployeeEntity.FullName = employeeViewModel.FullName;
				existingEmployeeEntity.Gender = employeeViewModel.Gender;
				existingEmployeeEntity.DOB = employeeViewModel.DOB;
				existingEmployeeEntity.DOE = employeeViewModel.DOE;
				existingEmployeeEntity.Phone = employeeViewModel.Phone;
				existingEmployeeEntity.Address = employeeViewModel.Address;
				existingEmployeeEntity.Salary = employeeViewModel.Salary;
				existingEmployeeEntity.Email = employeeViewModel.Email;
				existingEmployeeEntity.DOR = employeeViewModel.DOR;
				existingEmployeeEntity.CreatedAt = DateTime.Now;
				existingEmployeeEntity.CreatedBy = "System";
				existingEmployeeEntity.IsActive = true;
				existingEmployeeEntity.IpAddress = NetworkHelper.GetMechinePublicIP();

				_applicationDbContext.Employees.Update(existingEmployeeEntity);
				_applicationDbContext.SaveChanges();
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

		public string GetIpAddressofMachine()
		{
			return HttpContext.Connection.RemoteIpAddress.ToString();
		}
	}
}
