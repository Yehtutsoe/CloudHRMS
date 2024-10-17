using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Repositories
{
	public interface IAttendancePolicyRepository
	{
		void Create(AttendancePolicyViewModel attendancePolicyView);
		IList<AttendancePolicyViewModel> RetrieveAll();
		AttendancePolicyViewModel GetById(string Id);
		void Update(AttendancePolicyViewModel attendancePolicyView);
		void Delete(string Id);
	}
}
