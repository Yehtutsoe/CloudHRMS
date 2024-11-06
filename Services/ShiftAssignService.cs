using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories;

namespace CloudHRMS.Services
{
    public class ShiftAssignService : IShiftAssignService
    {
        private readonly IShiftAssignRepository _shiftAssignRepository;

        public ShiftAssignService(IShiftAssignRepository shiftAssignRepository)
        {
            _shiftAssignRepository = shiftAssignRepository;
        }
        public void Create(ShiftAssignViewModel shiftAssignViewModel)
        {
           _shiftAssignRepository.Create(shiftAssignViewModel);
        }

        public void Delete(string Id)
        {
            _shiftAssignRepository.Delete(Id);
        }

        public ShiftAssignViewModel GetById(string Id)
        {
            return _shiftAssignRepository.GetById(Id);
        }

        public IList<ShiftAssignViewModel> RetrieveAll()
        {
            return _shiftAssignRepository.RetrieveAll();
        }

        public void Update(ShiftAssignViewModel shiftAssignViewModel)
        {
            _shiftAssignRepository.Update(shiftAssignViewModel);
        }
    }
}
