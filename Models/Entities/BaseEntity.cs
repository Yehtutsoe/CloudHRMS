using CloudHRMS.Utility.Network;
using System.ComponentModel.DataAnnotations;

namespace CloudHRMS.Models.Entities
{
	public abstract class BaseEntity
	{
		[Key]
		public string Id { get; set; }
        [MaxLength(15)]
        public DateTime CreatedAt { get; set; }
		[MaxLength(15)]
		public string CreatedBy { get; set; }
		[MaxLength(15)]
		public DateTime? UpdatedAt { get; set; }
		[MaxLength(12)]
		public string? UpdatedBy { get; set; }
        public string IpAddress { get; set; } = NetworkHelper.GetMechinePublicIP();
		[MaxLength(6)]
		public bool IsActive { get; set; }
    }
}
