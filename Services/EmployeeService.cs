using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace CloudHRMS.Services
{
	public class EmployeeService : IEmployeeService
	{
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository, UserManager<IdentityUser> userManager)
        {
			_employeeRepository = employeeRepository;
            _userManager = userManager;
        }
		public async Task Create(EmployeeViewModel employeeView)
		{
                      
                var user = CreateUser();
                user.Email = employeeView.Email; //pass the email of employee data
                user.UserName = employeeView.Email; //pass the FullName of employee data
                user.NormalizedUserName = employeeView.FullName;
                user.NormalizedEmail = employeeView.Email;
                var result = await _userManager.CreateAsync(user, "Speci@l92"); // create a user with default password
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Employee"); // assign created user to "Employee" role
                     employeeView.UserId = user.Id;
                    _employeeRepository.Create(employeeView);
                 }
        }
            
            

		public void Delete(string Id)
		{
			_employeeRepository.Delete(Id);
		}

		public EmployeeViewModel GetById(string id)
		{
			return _employeeRepository.GetById(id);
		}

		public EmployeeViewModel PrepareEntryForm()
		{
			var employeeViewModel = new EmployeeViewModel
			{
				DepartmentsViewModel = _employeeRepository.GetActiveDepartment(),
				PositionsViewModel = _employeeRepository.GetActivePosition()
			};
			return employeeViewModel;
		}

		public IList<EmployeeViewModel> ReterieveAll()
		{
			return _employeeRepository.RetireveAll();
		}

		public void Update(EmployeeViewModel employeeView)
		{
			_employeeRepository.Update(employeeView);
		}

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
