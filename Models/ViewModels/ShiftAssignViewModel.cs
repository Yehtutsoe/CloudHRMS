﻿namespace CloudHRMS.Models.ViewModels
{
    public class ShiftAssignViewModel
    {
        public string Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string EmployeeId { get; set; } // CRUD Process
        public string EmployeeInfo { get; set; }
        public string ShiftId { get; set; } //CRUD Process
        public string ShiftInfo { get; set; }
        public IList<EmployeeViewModel> EmployeesViewModel { get; set; } // For UI Binding
        public IList<ShiftViewModel> ShiftsViewModel { get; set; } // For UI Binding


    }
}
