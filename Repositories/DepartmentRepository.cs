﻿using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility.Network;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CloudHRMS.Repositories
{
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;

		public DepartmentRepository(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}
		public void Create(DepartmentViewModel departmentView)
		{

			try
			{
				DepartmentEntity departmentEntity = new DepartmentEntity()
				{
					Id = Guid.NewGuid().ToString(),
					Code = departmentView.Code,
					Description = departmentView.Description,
					ExtensionPhone = departmentView.ExtensionPhone,
					IsActive = true,
					IpAddress = NetworkHelper.GetMechinePublicIP(),
					CreatedAt = DateTime.Now,
					CreatedBy = "System"

				};
				_applicationDbContext.Departments.Add(departmentEntity);
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
				var existingDepartments = _applicationDbContext.Departments.Where(w => w.Id == Id).SingleOrDefault();
				if (existingDepartments is not null)
				{
					existingDepartments.IsActive = false;
					_applicationDbContext.Update(existingDepartments);
					_applicationDbContext.SaveChanges();
				}
			}catch(Exception e)
			{
				throw e;
			}
		}

		public DepartmentViewModel GetById(string Id)
		{
			var department = _applicationDbContext.Departments.Where(w => w.Id == Id)
															.Select(s => new DepartmentViewModel
															{
																Id = s.Id,
																Code = s.Code,
																Description = s.Description,
																ExtensionPhone = s.ExtensionPhone

															}).SingleOrDefault();
			return department;
		}

		public IList<DepartmentViewModel> RetireveAll()
		{
			IList<DepartmentViewModel> departments = _applicationDbContext.Departments
														.Where(w => w.IsActive)
														.Select(s => new DepartmentViewModel
														{
															Id = s.Id,
															Code = s.Code,
															Description = s.Description,
															ExtensionPhone = s.ExtensionPhone
														}).ToList();
			return departments;
		}

		public void Update(DepartmentViewModel departmentView)
		{
			try
			{
				var existingDepartmentEntity = _applicationDbContext.Departments.Find(departmentView.Id);
				existingDepartmentEntity.Code = departmentView.Code;
				existingDepartmentEntity.Description = departmentView.Description;
				existingDepartmentEntity.ExtensionPhone = departmentView.ExtensionPhone;
				existingDepartmentEntity.CreatedBy = "system";
				existingDepartmentEntity.CreatedAt = DateTime.Now;
				existingDepartmentEntity.IsActive = true;
				existingDepartmentEntity.IpAddress = NetworkHelper.GetMechinePublicIP();
				_applicationDbContext.Departments.Update(existingDepartmentEntity);
				_applicationDbContext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}