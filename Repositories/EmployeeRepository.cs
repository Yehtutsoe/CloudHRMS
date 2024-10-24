using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using CloudHRMS.Utility.Network;

namespace CloudHRMS.Repositories
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;
		private readonly IUserService _userService;

		public EmployeeRepository(ApplicationDbContext applicationDbContext,IUserService userService)
        {
			_applicationDbContext = applicationDbContext;
			_userService = userService;
		}
		public async Task Create(EmployeeViewModel employeeViewModel)
		{
			try
			{
				string userId = await _userService.CreateUser(employeeViewModel.FullName,employeeViewModel.Email);
				if (string.IsNullOrEmpty(userId))
				{
					throw new Exception("User Creation Failsl.");
				}
					EmployeeEntity employeeEntity = new EmployeeEntity()
					{

						Id = Guid.NewGuid().ToString(),
						No = employeeViewModel.No,
						FullName = employeeViewModel.FullName,
						Gender = employeeViewModel.Gender,
						DOB = employeeViewModel.DOB,
						DOE = employeeViewModel.DOE,
						Phone = employeeViewModel.Phone,
						Address = employeeViewModel.Address,
						Salary = employeeViewModel.Salary,
						Email = employeeViewModel.Email,
						DOR = employeeViewModel.DOR,
						CreatedAt = DateTime.Now,
						CreatedBy = "System",
						IsActive = true,
						IpAddress = NetworkHelper.GetMechinePublicIP(),
						DepartmentId = employeeViewModel.DepartmentId,
						PositionId = employeeViewModel.PositionId,
						UserId = userId
					};

					_applicationDbContext.Employees.Add(employeeEntity);
					_applicationDbContext.SaveChanges();
				}
			catch(Exception e)
			{
				throw e;
			}
		}

		public void Delete(string Id)
		{
			try
			{
				var existingEmployee = _applicationDbContext.Employees.Where(w => w.Id == Id).SingleOrDefault();
				if (existingEmployee is not null)
				{
					existingEmployee.IsActive = false;
					_applicationDbContext.Update(existingEmployee);
					_applicationDbContext.SaveChanges();
				}
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public IList<DepartmentViewModel> GetActiveDepartment()
		{
			return _applicationDbContext.Departments.Where(w => w.IsActive).Select(s => new DepartmentViewModel
			{
				Id = s.Id,
				Code = s.Code
			}).ToList();
		}

		public IList<PositionViewModel> GetActivePosition()
		{
			return _applicationDbContext.Positions.Where(w => w.IsActive).Select(s => new PositionViewModel
			{
				Id = s.Id,
				Code = s.Code
			}).ToList();
		}

		public EmployeeViewModel GetById(string Id)
		{
			var employee = _applicationDbContext.Employees.Where(w => w.Id == Id)
														  .Select(s => new EmployeeViewModel
															{
																Id = s.Id,
																No = s.No,
																FullName = s.FullName,
																Email = s.Email,
																Phone = s.Phone,
																Address = s.Address,
																Salary = s.Salary,
																Gender = s.Gender,
																DOB = s.DOB,
																DOE = s.DOE,
																DOR = s.DOR
															}).SingleOrDefault();
			return employee;
		}

		public IList<EmployeeViewModel> RetireveAll()
		{
			IList<EmployeeViewModel> employees = (from e in _applicationDbContext.Employees
												  join d in _applicationDbContext.Departments
												  on e.DepartmentId equals d.Id
												  join p in _applicationDbContext.Positions
												  on e.PositionId equals p.Id
												  where e.IsActive & d.IsActive & p.IsActive
												  select new EmployeeViewModel
												  {
													Id= e.Id,
													FullName = e.FullName,
													Email = e.Email,
													Gender = e.Gender,
													DOB = e.DOB,
													DOE = e.DOE,
													DOR	= e.DOR,
													Phone = e.Phone,
													Address = e.Address,
													Salary = e.Salary,
													DepartmentId = e.DepartmentId,
													PositionId = e.PositionId,
													
												  }).ToList();
			return employees;
		}

		public void Update(EmployeeViewModel employeeViewModel)
		{
			try
			{
				var existingEmployeeEntity = _applicationDbContext.Employees.Find(employeeViewModel.Id);

				existingEmployeeEntity.No = employeeViewModel.No;
				existingEmployeeEntity.FullName = employeeViewModel.FullName;
				existingEmployeeEntity.Gender = employeeViewModel.Gender;
				existingEmployeeEntity.DOB = employeeViewModel.DOB;
				existingEmployeeEntity.DOE = employeeViewModel.DOE;
				existingEmployeeEntity.Phone = employeeViewModel.Phone;
				existingEmployeeEntity.Address = employeeViewModel.Address;
				existingEmployeeEntity.Salary = employeeViewModel.Salary;
				existingEmployeeEntity.Email = employeeViewModel.Email;
				existingEmployeeEntity.DOR = employeeViewModel.DOR;
				existingEmployeeEntity.CreatedAt = DateTime.Now;
				existingEmployeeEntity.CreatedBy = "System";
				existingEmployeeEntity.IsActive = true;
				existingEmployeeEntity.IpAddress = NetworkHelper.GetMechinePublicIP();

				_applicationDbContext.Employees.Update(existingEmployeeEntity);
				_applicationDbContext.SaveChanges();
			}
			catch(Exception e)
			{
				throw e;
			}
		}
	}
}
