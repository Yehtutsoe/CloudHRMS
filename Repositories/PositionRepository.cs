using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility.NetworkHelper;
using Microsoft.Extensions.Caching.Memory;

namespace CloudHRMS.Repositories
{
	public class PositionRepository : IPositionRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;
		private readonly IMemoryCache _cache;

		public PositionRepository(ApplicationDbContext applicationDbContext, IMemoryCache cache)
		{
			_applicationDbContext = applicationDbContext;
			_cache = cache;
		}
		public void Create(PositionViewModel positionView)
		{
			try
			{
				PositionEntity positionEntity = new PositionEntity()
				{
					Id = Guid.NewGuid().ToString(),
					Code = positionView.Code,
					Description = positionView.Description,
					Level = positionView.Level,
					CreatedAt = DateTime.Now,
					CreatedBy = "System",
					IsActive = true,
					IpAddress = NetworkHelper.GetMachinePublicIP(_cache)
				};
				_applicationDbContext.Positions.Add(positionEntity);
				_applicationDbContext.SaveChanges();
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public void Delete(string Id)
		{
			try
			{
				var existingPosition = _applicationDbContext.Positions.Where(w => w.Id == Id).SingleOrDefault();
				if (existingPosition != null)
				{
					existingPosition.IsActive = false;
					_applicationDbContext.Update(existingPosition);
					_applicationDbContext.SaveChanges();
				}
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public PositionViewModel GetById(string Id)
		{
			var position = _applicationDbContext.Positions
							.Where(p => p.Id == Id)
							.Select(s => new PositionViewModel
							{
								Id = s.Id,
								Code = s.Code,
								Description = s.Description,
								Level = s.Level
							}).SingleOrDefault();
			return position;
		}

		public IList<PositionViewModel> RetireveAll()
		{
			IList<PositionViewModel> positions = _applicationDbContext.Positions
												.Where(w => w.IsActive)
												.Select(s => new PositionViewModel
												{
													Id = s.Id,
													Code = s.Code,
													Description = s.Description,
													Level = s.Level

												}).ToList();
			return positions;
		}

		public void Update(PositionViewModel positionView)
		{
			try
			{
				var existingPositionEntity = _applicationDbContext.Positions.Find(positionView.Id);


				existingPositionEntity.Code = positionView.Code;
				existingPositionEntity.Description = positionView.Description;
				existingPositionEntity.Level = positionView.Level;
				existingPositionEntity.CreatedAt = DateTime.Now;
				existingPositionEntity.CreatedBy = "System";
				existingPositionEntity.IsActive = true;
				existingPositionEntity.IpAddress = NetworkHelper.GetMachinePublicIP(_cache);

				_applicationDbContext.Positions.Update(existingPositionEntity);
				_applicationDbContext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}

}