namespace CloudHRMS.Models.ViewModels
{
	public class EmployeeViewModel
	{
        public string Id { get; set; }
        public string No { get; set; }
		public string FullName { get; set; }
		public string Gender { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public DateTime DOB { get; set; }
		public decimal Salary { get; set; }
		public DateTime? DOE { get; set; } //Date of employee
		public DateTime? DOR { get; set; } // Date of retire
		public string Address { get; set; }
	}
}
