using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories;

namespace CloudHRMS.Services
{
	public class AttendancePolicyService : IAttendancePolicyService
	{
		private readonly IAttendancePolicyRepository _attendancePolicyRepository;

        public AttendancePolicyService(IAttendancePolicyRepository attendancePolicyRepository)
        {
            this._attendancePolicyRepository = attendancePolicyRepository;
        }
        public void Create(AttendancePolicyViewModel attendancePolicyView)
		{
			_attendancePolicyRepository.Create(attendancePolicyView);
		}

		public void Delete(string Id)
		{
			_attendancePolicyRepository.Delete(Id);
		}

		public AttendancePolicyViewModel GetById(string Id)
		{
			return _attendancePolicyRepository.GetById(Id);
		}

		public IList<AttendancePolicyViewModel> RetrieveAll()
		{
			return _attendancePolicyRepository.RetrieveAll();
		}

		public void Update(AttendancePolicyViewModel attendancePolicyView)
		{
			_attendancePolicyRepository.Update(attendancePolicyView);
		}
	}
}
