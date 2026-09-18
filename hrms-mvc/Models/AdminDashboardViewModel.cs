namespace hrms_mvc.Models
{
    public class AdminDashboardViewModel
    {
        public DateTime SelectedDate { get; set; }

        public List<DepartmentEmployeeViewModel> EmployeesByDepartment { get; set; } = new();

        public List<AttendanceDashboardViewModel> ClockInOutRecords { get; set; } = new();

        public List<EmployeeDashboardViewModel> Employees { get; set; } = new();

        public List<ProjectDashboardViewModel> Projects { get; set; } = new();

        public List<TaskStatisticsViewModel> TaskStatistics { get; set; } = new();
    }

    public class DepartmentEmployeeViewModel
    {
        public string DepartmentName { get; set; } = string.Empty;

        public int EmployeeCount { get; set; }
    }

    public class AttendanceDashboardViewModel
    {
        public int UserId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public DateTime? Checkin { get; set; }

        public DateTime? Checkout { get; set; }

        public string Status { get; set; } = string.Empty;
    }

    public class EmployeeDashboardViewModel
    {
        public int Id { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime DateOfJoining { get; set; }
    }

    public class ProjectDashboardViewModel
    {
        public int ProjectsId { get; set; }

        public string ProjectName { get; set; } = string.Empty;

        public string ClientName { get; set; } = string.Empty;

        public string ManagerName { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int TaskCount { get; set; }
    }

    public class TaskStatisticsViewModel
    {
        public string Status { get; set; } = string.Empty;

        public int Count { get; set; }
    }
}