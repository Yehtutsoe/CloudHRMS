using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility.NetworkHelper;
using Microsoft.Extensions.Caching.Memory;

namespace CloudHRMS.Repositories
{
    public class ShiftAssignRepository : IShiftAssignRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMemoryCache _cache;

        public ShiftAssignRepository(ApplicationDbContext applicationDbContext,IMemoryCache cache)
        {
            _applicationDbContext = applicationDbContext;
            _cache = cache;
        }
        public void Create(ShiftAssignViewModel shiftAssignViewModel)
        {
            try
            {
                ShiftAssignEntity shiftAssignEntity = new ShiftAssignEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    FromDate = shiftAssignViewModel.FromDate,
                    ToDate = shiftAssignViewModel.ToDate,
                    EmployeeId = shiftAssignViewModel.EmployeeId,
                    ShiftId = shiftAssignViewModel.ShiftId,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                    IpAddress = NetworkHelper.GetLocalIPAddress(),
                };
                _applicationDbContext.ShiftAssigns.Add(shiftAssignEntity);
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
                var existingShiftAssigns = _applicationDbContext.ShiftAssigns.Where(w => w.IsActive).SingleOrDefault();
                if (existingShiftAssigns is not null)
                {
                    existingShiftAssigns.IsActive = false;
                    _applicationDbContext.ShiftAssigns.Update(existingShiftAssigns);
                    _applicationDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

		public IList<EmployeeViewModel> GetActiveEmployee()
		{
            return _applicationDbContext.Employees.Where(w => w.IsActive).Select(s => new EmployeeViewModel {
                Id = s.Id,
                FullName =  s.FullName
            }).ToList();
		}

		public IList<ShiftViewModel> GetActiveShifts()
		{
           return _applicationDbContext.Shifts.Where(w => w.IsActive).Select(s => new ShiftViewModel
            {
               Id = s.Id,
               Name = s.Name

            }).ToList();
		}

		public ShiftAssignViewModel GetById(string Id)
        {
            var shiftAssigns = _applicationDbContext.ShiftAssigns.Where(w => w.Id == Id)
                                                                 .Select(s => new ShiftAssignViewModel
                                                                 {
                                                                     Id = s.Id,
                                                                     FromDate = s.FromDate,
                                                                     ToDate = s.ToDate,
                                                                     ShiftId = s.ShiftId,
                                                                     EmployeeId = s.EmployeeId,
                                                                 }).SingleOrDefault();
            return shiftAssigns;
        }

        public IList<ShiftAssignViewModel> RetrieveAll()
        {
            IList<ShiftAssignViewModel> shiftAssings = (from sa in _applicationDbContext.ShiftAssigns
                                                            join e in _applicationDbContext.Employees
                                                            on sa.EmployeeId equals e.Id
                                                            join s in _applicationDbContext.Shifts
                                                            on sa.ShiftId equals s.Id
                                                            where sa.IsActive && e.IsActive && s.IsActive
                                                            select new ShiftAssignViewModel
                                                            { 
                                                             Id = sa.Id, 
                                                             FromDate = sa.FromDate,
                                                             ToDate = sa.ToDate,
                                                             EmployeeId= sa.EmployeeId, // for CRUD Process
                                                             ShiftId = sa.ShiftId, // For CRUD Process
                                                             EmployeeInfo = e.FullName, //For UI Binding
                                                             ShiftInfo = s.Name, //For UI Binding

                                                            }).ToList();
            return shiftAssings;
        }

        public void Update(ShiftAssignViewModel shiftAssignViewModel)
        {
            var shiftAssignEntity = _applicationDbContext.ShiftAssigns.Find(shiftAssignViewModel.Id);
            shiftAssignEntity.FromDate = shiftAssignViewModel.FromDate;
            shiftAssignEntity.ToDate = shiftAssignViewModel.ToDate;
            shiftAssignEntity.UpdatedBy = "System";
            shiftAssignEntity.UpdatedAt = DateTime.Now;
            shiftAssignEntity.IsActive = true;
            shiftAssignEntity.EmployeeId = shiftAssignViewModel.EmployeeId;
            shiftAssignEntity.ShiftId = shiftAssignViewModel.ShiftId;
            shiftAssignEntity.IpAddress = NetworkHelper.GetMachinePublicIP(_cache);
            _applicationDbContext.ShiftAssigns.Add(shiftAssignEntity);
            _applicationDbContext.SaveChanges();
        }
    }
}
