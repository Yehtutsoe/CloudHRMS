﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.ViewModels
{
    public class DailyAttendanceViewModel
    {
        public string Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }
        public string EmployeeId { get; set; }
        public string DepartmentId { get; set; }
        public string EmployeeInfo { get; set; }
        public string DepartmentInfo { get; set; }

    }
}