using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories;
using Microsoft.Identity.Client;

namespace CloudHRMS.Services
{
	public class DailyAttendanceService : IDailyAttendanceService
	{
		private readonly IDailyAttendanceRepository _dailyAttendanceRepository;

        public DailyAttendanceService(IDailyAttendanceRepository dailyAttendanceRepository)
        {
			_dailyAttendanceRepository = dailyAttendanceRepository;
		}

		public DailyAttendanceViewModel PreparedEntryForm()
		{
			var dailAttendanceView = new DailyAttendanceViewModel
			{
				EmployeeViewModels = _dailyAttendanceRepository.GetActiveEmployee(),
				DepartmentViewModels = _dailyAttendanceRepository.GetActiveDepartment()
			};
			return dailAttendanceView;
		}
        public void Create(DailyAttendanceViewModel dailyAttendanceView)
		{
			_dailyAttendanceRepository.Create(dailyAttendanceView);
		}

		public void Delete(string Id)
		{
			_dailyAttendanceRepository.Delete(Id);
		}

		public DailyAttendanceViewModel GetById(string Id)
		{
			throw new NotImplementedException();
		}

		public IList<DailyAttendanceViewModel> ReterieveAll()
		{
			return _dailyAttendanceRepository.ReterieveAll();
		}

		public void Update(DailyAttendanceViewModel dailyAttendanceView)
		{
			throw new NotImplementedException();
		}
	}
}
