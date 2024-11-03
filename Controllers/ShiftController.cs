using CloudHRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class ShiftController : Controller
    {
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(ShiftViewModel shiftViewModel)
        {
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Edit(string Id)
        {
            return View();
        }
        public IActionResult Delete(string Id) {
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Update(ShiftViewModel shiftViewModel) {
            return RedirectToAction("List");
        }
    }
}
