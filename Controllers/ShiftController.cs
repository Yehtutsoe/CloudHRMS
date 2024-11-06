using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class ShiftController : Controller
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }
        [Authorize(Roles ="HR")]
        public IActionResult Entry()
        {
           return View(_shiftService.PreparedEntryForm());
        }
        [Authorize(Roles ="HR")]
        [HttpPost]
        public IActionResult Entry(ShiftViewModel shiftViewModel)
        {
            _shiftService.Create(shiftViewModel);
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var shift = _shiftService.RetrieveAll();
            return View(shift);
        }
        [Authorize(Roles = "HR")]
        public IActionResult Edit(string Id)
        {
            return View(_shiftService.GetById(Id));
        }
        [Authorize(Roles = "HR")]
        public IActionResult Delete(string Id) {

            _shiftService.Delete(Id);
            return RedirectToAction("List");
        }
        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult Update(ShiftViewModel shiftViewModel) {
            _shiftService.Update(shiftViewModel);
            return RedirectToAction("List");
        }
    }
}
