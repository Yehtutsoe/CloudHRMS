using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
    public interface IAttendanceMasterService
    {
        void DayEndProcess(AttendanceMasterViewModel attendanceMasterViewModel);
        IList<AttendanceMasterViewModel> RetrieveAll();
        AttendanceMasterViewModel PreparedFrom();
    }
}
