using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Repositories
{
	public class DailyAttendanceRepository : IDailyAttendanceRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;

        public DailyAttendanceRepository(ApplicationDbContext applicationDbContext)
        {
			_applicationDbContext = applicationDbContext;
        }
        public void Create(DailyAttendanceViewModel dailyAttendanceView)
		{
			try
			{
				DailyAttendanceEntity dailyAttendanceEntity = new DailyAttendanceEntity()
				{
					Id = dailyAttendanceView.Id,
					AttendanceDate = dailyAttendanceView.AttendanceDate,
					InTime = dailyAttendanceView.InTime,
					OutTime = dailyAttendanceView.OutTime,
					CreatedBy = "System",
					CreatedAt = DateTime.Now,
					IsActive = true,
					DepartmentId = dailyAttendanceView.DepartmentId,
					EmployeeId = dailyAttendanceView.EmployeeId
				};
				_applicationDbContext.Add(dailyAttendanceEntity);
				_applicationDbContext.SaveChanges();
			}
			catch (Exception e)
			{

				throw e;
			}
		}

		public void Delete(string Id)
		{
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
