using CloudHRMS.DAO;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories;

namespace CloudHRMS.Services
{
	public class PositionService : IPositionService
	{
		private readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }
        public void Create(PositionViewModel positionView)
		{
			_positionRepository.Create(positionView);
		}

		public void Delete(string Id)
		{
			_positionRepository.Delete(Id);
		}

		public PositionViewModel GetById(string Id)
		{
			return _positionRepository.GetById(Id);
		}

		public IList<PositionViewModel> RetireveAll()
		{
			return _positionRepository.RetireveAll();
		}

		public void Update(PositionViewModel positionView)
		{
			_positionRepository.Update(positionView);
		}
	}
}
