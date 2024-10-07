using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
	public class PositionController : Controller
	{
		private readonly ApplicationDbContext _applicationDbContext;

		ErrorViewModel error = new ErrorViewModel();

        public PositionController(ApplicationDbContext applicationDbContext)
        {
			_applicationDbContext = applicationDbContext;
        }
        public IActionResult PositionEntry()

		{
			return View();
		}

		[HttpPost]
		public IActionResult PositionEntry(PositionViewModel positionViewModel)
		{
			try
			{
				PositionEntity positionEntity = new PositionEntity()
				{
					Id = Guid.NewGuid().ToString(),
					Code = positionViewModel.Code,
					Description = positionViewModel.Description,
					Level = positionViewModel.Level,
					CreatedAt = DateTime.Now,
					CreatedBy = "System",
					IsActive = true,
					IpAddress = GetIpAddressofMachine()
				};
				_applicationDbContext.Positions.Add(positionEntity);
				_applicationDbContext.SaveChanges();
				error.Message = "Successful save the record to the system ";
			}
			catch (Exception ex)
			{
				error.Message = "Error Occur";
				error.IsOccurError = true;
			}
			ViewBag.Msg = error;
			return View();
		}

		public IActionResult List()
		{
			IList<PositionViewModel> positions = _applicationDbContext.Positions
												.Where(w => w.IsActive)
												.Select(s => new PositionViewModel { 
													Id = s.Id,
													Code = s.Code,
													Description = s.Description,
													Level = s.Level
																									
												}).ToList();
			return View(positions);
		}

		public string GetIpAddressofMachine()
		{
			return HttpContext.Connection.RemoteIpAddress.ToString();
		}
	}
}
