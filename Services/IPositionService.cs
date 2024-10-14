using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
	public interface IPositionService
	{
		void Create(PositionViewModel positionView);
		PositionViewModel GetById(string Id);
		IList<PositionViewModel> RetireveAll();
		void Update(PositionViewModel positionView);
		void Delete(string Id);
	}
}
