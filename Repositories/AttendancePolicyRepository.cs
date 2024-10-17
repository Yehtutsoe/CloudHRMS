
using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility.Network;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CloudHRMS.Repositories
{
	public class AttendancePolicyRepository : IAttendancePolicyRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;

        public AttendancePolicyRepository(ApplicationDbContext applicationDbContext)
        {
			_applicationDbContext = applicationDbContext;
        }
        public void Create(AttendancePolicyViewModel attendancePolicyView)
		{
			try
			{
				AttendancePolicyEntity attendancePolicyEntity = new AttendancePolicyEntity()
				{
					Id = Guid.NewGuid().ToString(),
					Name = attendancePolicyView.Name,
					NumberOfLateTimes = attendancePolicyView.NumberOfLateTimes,
					NumberOfEarlyOutTimes = attendancePolicyView.NumberOfEarlyOutTimes,
					DeductionInAmount = attendancePolicyView.DeductionInAmount,
					DeductionInDay = attendancePolicyView.DeductionInDay,
					CreatedAt = DateTime.Now,
					CreatedBy = "System",
					IsActive = true,
					IpAddress = NetworkHelper.GetMechinePublicIP(),
					UpdatedAt = DateTime.Now,
					UpdatedBy = "System"
				};
				_applicationDbContext.Add(attendancePolicyEntity);
				_applicationDbContext.SaveChanges();
			}
			catch (Exception e)
			{

				throw e;
			}
			
		}

		public void Delete(string Id)
		{
			var existingAttendance = _applicationDbContext.AttendancePolicys.Where(w => w.Id == Id).SingleOrDefault();
			if (existingAttendance != null)
			{
				existingAttendance.IsActive = false;
				_applicationDbContext.Update(existingAttendance);
				_applicationDbContext.SaveChanges();
			}
		}

		public AttendancePolicyViewModel GetById(string Id)
		{
			var attendancePolicy = _applicationDbContext.AttendancePolicys
													.Where(w => w.Id == Id)
													.Select(s => new AttendancePolicyViewModel
													{
														Name = s.Name,
														NumberOfLateTimes = s.NumberOfLateTimes,
														NumberOfEarlyOutTimes = s.NumberOfEarlyOutTimes,
														DeductionInAmount = s.DeductionInAmount,
														DeductionInDay = s.DeductionInDay
													}).SingleOrDefault();
			return attendancePolicy;
		}

		public IList<AttendancePolicyViewModel> RetrieveAll()
		{
			IList<AttendancePolicyViewModel> attendancePolicy = _applicationDbContext.AttendancePolicys
																					 .Where(w => w.IsActive)
																					 .Select(s => new AttendancePolicyViewModel
																					 {
																						 Name = s.Name,
																						 NumberOfLateTimes = s.NumberOfLateTimes,
																						 NumberOfEarlyOutTimes = s.NumberOfEarlyOutTimes,
																						 DeductionInAmount = s.DeductionInAmount,
																						 DeductionInDay = s.DeductionInDay
																					 }).ToList();
			return attendancePolicy ;
		}

		public void Update(AttendancePolicyViewModel attendancePolicyView)
		{
			try
			{
				var existingAttendancePolicy = _applicationDbContext.AttendancePolicys.Find(attendancePolicyView.Id);
				existingAttendancePolicy.Name = attendancePolicyView.Name;
				existingAttendancePolicy.NumberOfLateTimes = attendancePolicyView.NumberOfLateTimes;
				existingAttendancePolicy.NumberOfEarlyOutTimes = attendancePolicyView.NumberOfEarlyOutTimes;
				existingAttendancePolicy.DeductionInAmount = attendancePolicyView.DeductionInAmount;
				existingAttendancePolicy.DeductionInDay = attendancePolicyView.DeductionInDay;
				existingAttendancePolicy.CreatedBy = "System";
				existingAttendancePolicy.CreatedAt = DateTime.Now;
				existingAttendancePolicy.IsActive = true;
				existingAttendancePolicy.IpAddress = NetworkHelper.GetMechinePublicIP();
				_applicationDbContext.Update(existingAttendancePolicy);
				_applicationDbContext.SaveChanges();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
	}
}
