    namespace hrms_mvc.Models
    {
        public class EmployeeDashboardViewModel
        {
            public DateTime SelectedDate { get; set; }

            public User? Employee { get; set; }

            public int TotalLeave { get; set; }
            public int TakenLeave { get; set; }
            public int AbsentLeave { get; set; }
            public int SickLeave { get; set; }
            public int WorkedDays { get; set; }
            public int LossOfPay { get; set; }

            public decimal TotalWorkingHours { get; set; }
            public decimal ProductiveHours { get; set; }
            public decimal BreakHours { get; set; }
            public decimal OvertimeHours { get; set; }

            public List<EmployeeProjectViewModel> Projects { get; set; } = new();

            public List<EmployeeTaskViewModel> Tasks { get; set; } = new();
        }

        public class EmployeeProjectViewModel
        {
            public int ProjectId { get; set; }
            public string? ProjectName { get; set; }
            public string? ClientName { get; set; }
            public string? Status { get; set; }
            public int TotalTasks { get; set; }
        }

        public class EmployeeTaskViewModel
        {
            public int TaskId { get; set; }
            public string? Title { get; set; }
            public string? ProjectName { get; set; }
            public string? Status { get; set; }
            public string? Priority { get; set; }
            public DateTime Deadline { get; set; }
        }
    }