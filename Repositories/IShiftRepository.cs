using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Repositories
{
    public interface IShiftRepository
    {
        IList<AttendancePolicyViewModel> GetActiveAttendancePolicies();
        void Create(ShiftViewModel shiftViewModel);
        ShiftViewModel GetById(string Id);
        IList<ShiftViewModel> RetrieveAll();
        void Update(ShiftViewModel shiftViewModel);
        void Delete(String Id);

    }
}
