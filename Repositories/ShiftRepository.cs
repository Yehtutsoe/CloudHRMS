using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility.NetworkHelper;
using Microsoft.Extensions.Caching.Memory;

namespace CloudHRMS.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMemoryCache _cache;

        #region Constructor
        public ShiftRepository(ApplicationDbContext applicationDbContext,IMemoryCache cache)
        {
            _applicationDbContext = applicationDbContext;
            _cache = cache;
        }
        #endregion

        public IList<AttendancePolicyViewModel> GetActiveAttendancePolicies() {
            return _applicationDbContext.Shifts.Where(w => w.IsActive).Select(s => new AttendancePolicyViewModel
            {
                Id = s.AttendancePolicyId,
                Name = s.Name
            }).ToList();
        }

        #region Create
        public void Create(ShiftViewModel shiftViewModel)
        {
            try
            {
                ShiftEntity shiftEntity = new ShiftEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = shiftViewModel.Name,
                    InTime = shiftViewModel.InTime,
                    OutTime = shiftViewModel.OutTime,
                    EarlyOutBefore = shiftViewModel.EarlyOutBefore,
                    LateAfter = shiftViewModel.LateAfter,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    IpAddress = NetworkHelper.GetMachinePublicIP(_cache),
                    IsActive = true,
                    AttendancePolicyId = shiftViewModel.AttendancePolicyId,
                };
                _applicationDbContext.Shifts.Add(shiftEntity);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion

        #region Delete
        public void Delete(string Id)
        {
            try
            {
                var existingShift = _applicationDbContext.Shifts.Where(w => w.Id == Id).FirstOrDefault();
                if (existingShift is not null)
                {
                    existingShift.IsActive = false;
                    _applicationDbContext.Update(existingShift);
                    _applicationDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion

        #region GeyById
        public ShiftViewModel GetById(string Id)
        {
            var shifts = _applicationDbContext.Shifts.Where(w => w.Id == Id)
                                                     .Select(s => new ShiftViewModel { 
                                                        Id =s.Id,
                                                        Name = s.Name,
                                                        InTime = s.InTime,
                                                        OutTime = s.OutTime,
                                                        LateAfter = s.LateAfter,
                                                        EarlyOutBefore = s.EarlyOutBefore,                                                      
                                                     }).SingleOrDefault();
            return shifts;
        }
        #endregion

        public IList<ShiftViewModel> RetrieveAll()
        {
            return null;
        }

        #region Update
        public void Update(ShiftViewModel shiftViewModel)
        {
            var existShiftsEntity = _applicationDbContext.Shifts.Find(shiftViewModel.Id);
            existShiftsEntity.Name = shiftViewModel.Name;
            existShiftsEntity.InTime = shiftViewModel.InTime;
            existShiftsEntity.OutTime = shiftViewModel.OutTime;
            existShiftsEntity.LateAfter = shiftViewModel.LateAfter;
            existShiftsEntity.EarlyOutBefore = shiftViewModel.EarlyOutBefore;
            existShiftsEntity.UpdatedBy = "System";
            existShiftsEntity.UpdatedAt = DateTime.Now;
            existShiftsEntity.IsActive = true;
            existShiftsEntity.IpAddress = NetworkHelper.GetMachinePublicIP(_cache);

        }
        #endregion
    }
}
