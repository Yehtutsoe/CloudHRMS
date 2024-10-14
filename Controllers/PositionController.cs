using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using CloudHRMS.Utility.Network;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
	public class PositionController : Controller
	{
		private readonly IPositionService _positionService;	
		ErrorViewModel error = new ErrorViewModel();

        public PositionController(IPositionService positionService)
        {
			this._positionService = positionService;
        }
        public IActionResult Entry()=>View();

		[HttpPost]
		public IActionResult Entry(PositionViewModel positionViewModel)
		{
			try {
				_positionService.Create(positionViewModel);
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
		public IActionResult List()=>View();
		public IActionResult Edit(string Id)=>View(_positionService.GetById(Id));
		[HttpPost]
		public IActionResult Update(PositionViewModel positionViewModel)
		{
			try
			{
				_positionService.Update(positionViewModel);
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
			try {
					_positionService.Delete(Id);
					TempData["Msg"] = "Successful Delete from System";
					TempData["IsOccourError"] = false;
			}
			catch (Exception)
			{
				TempData["Msg"] = "Error Occour";
				TempData["IsOccourError"] = true;
			}
			return RedirectToAction("List");
		}
	}
}
