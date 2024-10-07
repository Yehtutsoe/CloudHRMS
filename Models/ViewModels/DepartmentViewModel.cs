using System.ComponentModel.DataAnnotations;

namespace CloudHRMS.Models.ViewModels
{
	public class DepartmentViewModel
	{
		[Key]
        public string Id { get; set; }
        public string Code { get; set; }
		public string Description { get; set; }
		public string ExtensionPhone { get; set; }
	}
}
