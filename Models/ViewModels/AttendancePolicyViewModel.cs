namespace CloudHRMS.Models.ViewModels
{
	public class AttendancePolicyViewModel
	{
        public string Id { get; set; }
        public string Name { get; set; }
		public int NumberOfLateTimes { get; set; }
		public int NumberOfEarlyOutTimes { get; set; }
		public Decimal DeductionInAmount { get; set; }
		public int DeductionInDay { get; set; }
	}
}
