using CloudHRMS.DAO;
using CloudHRMS.Repositories;
using CloudHRMS.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var config = builder.Configuration; //declare the configure to read json
//add the dbContext that we defined the ApplicationDbContext to get connestion string
builder.Services.AddDbContext<ApplicationDbContext>(option =>option
															.UseSqlServer(config.GetConnectionString("CloudHRMSConnectionString")));
// Declare Service and repository Interface
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAttendancePolicyService, AttendancePolicyService>();
builder.Services.AddScoped<IAttendancePolicyRepository, AttendancePolicyRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
