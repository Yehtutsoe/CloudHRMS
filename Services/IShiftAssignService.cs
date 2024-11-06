using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
    public interface IShiftAssignService
    {
        public void Create(ShiftAssignViewModel shiftAssignViewModel);
        ShiftAssignViewModel GetById(string Id);
        IList<ShiftAssignViewModel> RetrieveAll();
        public void Delete(string Id);
        public void Update(ShiftAssignViewModel shiftAssignViewModel);
    }
}
