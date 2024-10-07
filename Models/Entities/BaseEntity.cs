using System.ComponentModel.DataAnnotations;

namespace CloudHRMS.Models.Entities
{
	public abstract class BaseEntity
	{
		[Key]
		public string Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; } 
        public string? UpdatedBy { get; set; }
        public string IpAddress { get; set; }
        public bool IsActive { get; set; }
    }
}
