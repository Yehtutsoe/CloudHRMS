using CloudHRMS.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace CloudHRMS.DAO
{
	public class ApplicationDbContext:DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option):base(option)
        {
            
        }
        //Define the DbSet what we want for domains

        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<PositionEntity> Positions { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
    }
}
