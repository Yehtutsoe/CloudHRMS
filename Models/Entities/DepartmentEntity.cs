using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.Entities
{
	[Table("Department")]
	public class DepartmentEntity:BaseEntity
	{
        [MaxLength(8)]
        public string Code { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        [MaxLength(12)]
        public string ExtensionPhone { get; set; }

    }
}
