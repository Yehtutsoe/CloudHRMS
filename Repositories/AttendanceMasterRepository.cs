using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility.NetworkHelper;
using Microsoft.Extensions.Caching.Memory;

namespace CloudHRMS.Repositories
{
	public class AttendanceMasterRepository : IAttendanceMasterRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;
		private readonly IMemoryCache _cache;
		public AttendanceMasterRepository(ApplicationDbContext applicationDbContext,IMemoryCache cache)
        {
			_applicationDbContext = applicationDbContext;
			_cache = cache;
        }

        public IMemoryCache Cache { get; }
        #region DayEndProcess
        public void DayEndProcess(AttendanceMasterViewModel attendanceMasterViewModel)
		{
			IList<AttendanceMasterEntity> attendanceMasterEntities = new List<AttendanceMasterEntity>();
			var dailyAttendanceWithShiftAssignData = (from d in _applicationDbContext.DailyAttendances
													  join sa in _applicationDbContext.ShiftAssigns
													  on d.EmployeeId equals sa.EmployeeId
													  where sa.EmployeeId == attendanceMasterViewModel.EmployeeId && 
													  (attendanceMasterViewModel.AttendanceDate >= sa.FromDate && sa.FromDate <= attendanceMasterViewModel.ToDate)
													  select new
													  {
														  dailyAttendances = d,
														  shiftAssigns = sa
													  }).ToList();
			foreach(var data in dailyAttendanceWithShiftAssignData)
			{
				ShiftEntity defineShift = _applicationDbContext.Shifts.Where(w => w.Id == data.shiftAssigns.ShiftId).SingleOrDefault();
				if(defineShift is not null)
				{
					AttendanceMasterEntity attendanceMaster = new AttendanceMasterEntity();
					attendanceMaster.Id = Guid.NewGuid().ToString();
					attendanceMaster.CreatedAt = DateTime.Now;
					attendanceMaster.CreatedBy = "System";
					attendanceMaster.IsLeave = false;
					attendanceMaster.IpAddress = NetworkHelper.GetMachinePublicIP(_cache);
					attendanceMaster.InTime = data.dailyAttendances.InTime;
					attendanceMaster.OutTime = data.dailyAttendances.OutTime;
					attendanceMaster.EmployeeId = data.dailyAttendances.EmployeeId;
					attendanceMaster.ShiftId = defineShift.Id;
					attendanceMaster.DepartmentId = data.dailyAttendances.DepartmentId;
					attendanceMaster.AttendanceDate = data.dailyAttendances.AttendanceDate;

					// checking In Time
					if(data.dailyAttendances.InTime > defineShift.LateAfter) // 9:15 >= 9:10 
					{
						attendanceMaster.IsLate = true;
					}
					else
					{
						attendanceMaster.IsLate = false;
					}
						// Cheching Out Time
					if (data.dailyAttendances.OutTime < defineShift.EarlyOutBefore) // 5:00 <= 4:50
					{
						attendanceMaster.IsEarlyOut = true;
					}
					else
					{
						attendanceMaster.IsEarlyOut = false;
					}
					attendanceMasterEntities.Add(attendanceMaster);
				}
				
			} // End of foreach
            _applicationDbContext.AttendanceMasters.AddRange(attendanceMasterEntities);
            _applicationDbContext.SaveChanges();
        }
        #endregion

        #region GetActiveView
        public IList<DepartmentViewModel> GetActiveDeparment()
        {
			return _applicationDbContext.Departments.Where(w => w.IsActive).Select(s => new DepartmentViewModel
			{
				Id = s.Id,
				Code = s.Code + "|" + s.Description

			}).ToList();
            
        }

        public IList<EmployeeViewModel> GetActiveEmployees()
        {
            return _applicationDbContext.Employees.Where(w => w.IsActive).Select(s => new EmployeeViewModel
            {
                Id = s.Id,
                FullName = s.No + "|" + s.FullName
            }).ToList();
        }

        public IList<ShiftViewModel> GetActiveShift()
        {
            return _applicationDbContext.Shifts.Where(w => w.IsActive).Select(s => new ShiftViewModel
            {
                Id = s.Id,
                Name = s.Name

            }).ToList();
        }
        #endregion

        #region Retrieve
        public IList<AttendanceMasterViewModel> RetrieveAll()
		{
			var attendanceMaster = (from am in _applicationDbContext.AttendanceMasters
									join e in _applicationDbContext.Employees on am.EmployeeId equals e.Id
									join d in _applicationDbContext.Departments on am.DepartmentId equals d.Id
									join s in _applicationDbContext.Shifts on am.ShiftId equals s.Id
									select new AttendanceMasterViewModel
									{
										Id = am.Id,
										AttendanceDate = am.AttendanceDate,
										InTime = am.InTime,
										OutTime = am.OutTime,
										IsLate = am.IsLate,
										IsEarlyOut = am.IsEarlyOut,
										DepartmentId =d.Description,
										EmployeeId = e.No + "|" + e.FullName,
										ShiftId = s.Name

									}).ToList();
			return attendanceMaster;
		}
        #endregion
    }
}
