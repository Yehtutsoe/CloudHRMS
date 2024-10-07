using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
			catch (Exception ex)
			{

				error.Message = "Transaction not save,found error";
				error.IsOccurError = true;
			}
			ViewBag.Msg = error;

			return View();
		}

		public string GetIpAddressofMachine()
		{
			return HttpContext.Connection.RemoteIpAddress.ToString();
		}
	}
}
