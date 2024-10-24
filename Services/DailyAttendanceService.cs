using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories;

namespace CloudHRMS.Services
{
	public class DailyAttendanceService : IDailyAttendanceService
	{
		private readonly IDailyAttendanceRepository _dailyAttendanceRepository;

        public DailyAttendanceService(IDailyAttendanceRepository dailyAttendanceRepository)
        {
			_dailyAttendanceRepository = dailyAttendanceRepository;
		}
        public void Create(DailyAttendanceViewModel dailyAttendanceView)
		{
			_dailyAttendanceRepository.Create(dailyAttendanceView);
		}

		public void Delete(string Id)
		{
			throw new NotImplementedException();
		}

		public DailyAttendanceViewModel GetById(string Id)
		{
			throw new NotImplementedException();
		}

		public IList<DailyAttendanceViewModel> ReterieveAll()
		{
			throw new NotImplementedException();
		}

		public void Update(DailyAttendanceViewModel dailyAttendanceView)
		{
			throw new NotImplementedException();
		}
	}
}
