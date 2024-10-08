﻿using CloudHRMS.DAO;
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
			catch
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

		public IActionResult Edit(string Id)

		{
			var position = _applicationDbContext.Positions
							.Where(p => p.Id == Id)
							.Select(s => new PositionViewModel
							{
								Id = s.Id,
								Code = s.Code,
								Description = s.Description,
								Level = s.Level
							}).SingleOrDefault();
			return View(position);
		}
		[HttpPost]
		public IActionResult Update(PositionViewModel positionViewModel)
		{
			try
			{
				PositionEntity positions = new PositionEntity()
				{
					Id = positionViewModel.Id,
					Code = positionViewModel.Code,
					Description = positionViewModel.Description,
					Level = positionViewModel.Level,
					CreatedAt = DateTime.Now,
					CreatedBy = "System",
					IsActive = true,
					IpAddress = GetIpAddressofMachine()
				};
				_applicationDbContext.Positions.Update(positions);
				_applicationDbContext.SaveChanges();
				TempData["Msg"] = "Successful update to sytem";
				TempData["IsOccourError"] = false;
			}
			catch (Exception)
			{
				TempData["Msg"] = "Error Occour";
				TempData["Msg"] = true;
				
			}
			return RedirectToAction("List");
		}

		public IActionResult Delete(string Id)
		{
			try
			{
				var existingPosition = _applicationDbContext.Positions.Where(w => w.Id == Id).SingleOrDefault();
				if (existingPosition != null)
				{
					existingPosition.IsActive = false;
					_applicationDbContext.Update(existingPosition);
					_applicationDbContext.SaveChanges();
					TempData["Msg"] = "Successful Delete from System";
					TempData["IsOccourError"] = false;
					
				}
			}
			catch (Exception)
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
