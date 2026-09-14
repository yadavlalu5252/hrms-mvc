using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class Projects
    {
        [Key]
        public int ProjectsId { get; set; }

        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Priority { get; set; }
        public double ProjectValue { get; set; }
        public string PriceType { get; set; }
        public string FilePath { get; set; }
        public string LogoPath { get; set; }
        public string Status { get; set; }
        public string ManagerName { get; set; }
        public List<Tasks> Tasks { get; set; }
        

    }
}
