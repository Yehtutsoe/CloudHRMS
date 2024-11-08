using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class ShiftAssignController : Controller
    {
        private readonly IShiftAssignService _shiftAssignService;

        public ShiftAssignController(IShiftAssignService shiftAssignService)
        {
            _shiftAssignService = shiftAssignService;
        }
        public IActionResult Entry()
        {
            return View(_shiftAssignService.PreparyEntryForm());
        }
        [Authorize(Roles ="HR")]
        [HttpPost]
        public IActionResult Entry(ShiftAssignViewModel shiftAssignViewModel)
        {
            _shiftAssignService.Create(shiftAssignViewModel);
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var shiftAssign = _shiftAssignService.RetrieveAll();
            return View(shiftAssign);
        }
        [Authorize(Roles ="HR")]
        public IActionResult Edit(string Id)
        {
            return View(_shiftAssignService.GetById(Id));
        }
        public IActionResult Update(ShiftAssignViewModel shiftAssignViewModel) {
            _shiftAssignService.Update(shiftAssignViewModel);
            return RedirectToAction("List");
        }
    }
}
