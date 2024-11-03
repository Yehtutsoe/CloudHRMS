using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories;


namespace CloudHRMS.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;
       
        public ShiftService(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }
        public void Create(ShiftViewModel shiftViewModel)
        {
            _shiftRepository.Create(shiftViewModel);
        }

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public ShiftViewModel GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public ShiftViewModel PreparedEntryForm()
        {
            var shiftViewModel = new ShiftViewModel()
            {
                AttendancePolicyViewsModel=_shiftRepository.GetActiveAttendancePolicies()
            };
            return shiftViewModel;
        }

        public IList<ShiftViewModel> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public void Update(ShiftViewModel shiftViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
