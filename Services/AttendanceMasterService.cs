using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories;
using Humanizer;

namespace CloudHRMS.Services
{
    public class AttendanceMasterService : IAttendanceMasterService
    {
        private readonly IAttendanceMasterRepository _attendanceMasterRepository;

        public AttendanceMasterService(IAttendanceMasterRepository attendanceMasterRepository)
        {
            _attendanceMasterRepository = attendanceMasterRepository;
        }
        public void DayEndProcess(AttendanceMasterViewModel attendanceMasterViewModel)
        {
            _attendanceMasterRepository.DayEndProcess(attendanceMasterViewModel);
        }

        public AttendanceMasterViewModel PreparedFrom()
        {
            var attendanceMasterView = new AttendanceMasterViewModel()
            {
                DepartmentViewModels = _attendanceMasterRepository.GetActiveDeparment(),
                EmployeeViewModels = _attendanceMasterRepository.GetActiveEmployees(),
                ShiftViewModels = _attendanceMasterRepository.GetActiveShift()
            };
            return attendanceMasterView;

        }

        public IList<AttendanceMasterViewModel> RetrieveAll()
        {
            return _attendanceMasterRepository.RetrieveAll();
        }
    }
}
