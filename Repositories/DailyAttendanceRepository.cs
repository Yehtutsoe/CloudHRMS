using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility.Network;

namespace CloudHRMS.Repositories
{
	public class DailyAttendanceRepository : IDailyAttendanceRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;

        public DailyAttendanceRepository(ApplicationDbContext applicationDbContext)
        {
			_applicationDbContext = applicationDbContext;
        }

		public IList<EmployeeViewModel> GetActiveEmployee()
		{
			return _applicationDbContext.Employees.Where(w => w.IsActive).Select(s => new EmployeeViewModel
			{
				Id = s.Id,
				No = s.No + "|" + s.FullName
			}).ToList();
		}
		public IList<DepartmentViewModel> GetAcitiveDepartment()
		{
			return _applicationDbContext.Departments.Where(w => w.IsActive).Select(s => new DepartmentViewModel
			{
				Id = s.Id,
				Code = s.Code + "|" + s.Description
			}).ToList();
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
			var existingDailyAttendance = _applicationDbContext.DailyAttendances.Where(w => w.Id == Id).SingleOrDefault();
			if(existingDailyAttendance is not null)
			{
				existingDailyAttendance.IsActive = false;
				_applicationDbContext.Update(existingDailyAttendance);
				_applicationDbContext.SaveChanges();
			}									
		}

		public DailyAttendanceViewModel GetById(string Id)
		{
			var dailyAttendance = _applicationDbContext.DailyAttendances.Where(w => w.Id == Id)
																		.Select(s => new DailyAttendanceViewModel
																		{
																			Id = s.Id,
																			AttendanceDate = s.AttendanceDate,
																			OutTime = s.OutTime,
																			InTime	= s.InTime
																		}).SingleOrDefault();
			return dailyAttendance;
		}

		public IList<DailyAttendanceViewModel> ReterieveAll()
		{
			IList<DailyAttendanceViewModel> dailyAttendance = (from da in _applicationDbContext.DailyAttendances
															   join e in _applicationDbContext.Employees 
															   on da.EmployeeId equals e.Id
															   join d in _applicationDbContext.Departments
															   on da.DepartmentId equals d.Id
																where da.IsActive && e.IsActive && d.IsActive
															   select new DailyAttendanceViewModel
															   {
																   Id = da.Id,
																   AttendanceDate = da.AttendanceDate,
																   InTime = da.InTime,
																   OutTime = da.OutTime,
																   DepartmentInfo = d.Description + "|" + d.Code,
																   EmployeeInfo = e.No + "|" + e.FullName
															   }).ToList();
			return dailyAttendance;
		}

		public void Update(DailyAttendanceViewModel dailyAttendanceView)
		{

			try
			{
				var existingDailyAttendace = _applicationDbContext.DailyAttendances.Find(dailyAttendanceView.Id);
				existingDailyAttendace.AttendanceDate = dailyAttendanceView.AttendanceDate;
				existingDailyAttendace.InTime = dailyAttendanceView.InTime;
				existingDailyAttendace.OutTime = dailyAttendanceView.OutTime;
				existingDailyAttendace.IsActive = true;
				existingDailyAttendace.UpdatedBy = "System";
				existingDailyAttendace.UpdatedAt = DateTime.Now;
				existingDailyAttendace.IpAddress = NetworkHelper.GetMechinePublicIP();
				_applicationDbContext.Update(existingDailyAttendace);
				_applicationDbContext.SaveChanges();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
	}
}
