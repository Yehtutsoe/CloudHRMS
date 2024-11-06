using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Repositories
{
    public interface IShiftAssignRepository
    {
        public void Create(ShiftAssignViewModel shiftAssignViewModel);
        ShiftAssignViewModel GetById(string Id);
        IList<ShiftAssignViewModel> RetrieveAll();
        public void Delete(string Id);
        public void Update(ShiftAssignViewModel shiftAssignViewModel);
    }
}
