using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.Entities
{
    [Table("Position")]
	public class PositionEntity:BaseEntity
	{
        [MaxLength(10)]
        public string Code { get; set; }
        [MaxLength(25)]
        public string Description { get; set; }
        [MaxLength(3)]
        public int Level { get; set; }  
    }
}
