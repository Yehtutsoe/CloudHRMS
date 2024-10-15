using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility.Network;

namespace CloudHRMS.Repositories
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;
        public EmployeeRepository(ApplicationDbContext applicationDbContext)
        {
			_applicationDbContext = applicationDbContext;
        }
		public void Create(EmployeeViewModel employeeViewModel)
		{
			try
			{
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
					PositionId = employeeViewModel.PositionId
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
			IList<EmployeeViewModel> employees = _applicationDbContext.Employees
														.Where(w => w.IsActive)
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
