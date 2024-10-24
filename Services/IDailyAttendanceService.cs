using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
	public interface IDailyAttendanceService
	{
		void Create(DailyAttendanceViewModel dailyAttendanceView);
		IList<DailyAttendanceViewModel> ReterieveAll();
		DailyAttendanceViewModel GetById(string Id);
		void Update(DailyAttendanceViewModel dailyAttendanceView);
		void Delete(string Id);
	}
}
