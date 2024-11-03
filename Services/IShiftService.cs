using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
    public interface IShiftService
    {
        ShiftViewModel PreparedEntryForm();
        void Create(ShiftViewModel shiftViewModel);
        IList<ShiftViewModel> RetrieveAll();
        ShiftViewModel GetById(string Id);
        void Update(ShiftViewModel shiftViewModel);
        void Delete(string Id);
    }
}
