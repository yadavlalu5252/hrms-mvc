using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    
        public class Tasks
        {
            [Key]
            public int TasksId { get; set; }
            
            [ForeignKey("Project")]
            public int ProjectId { get; set; }


            public Projects Project { get; set; }

            
            public string Title { get; set; }

            
            public string Description { get; set; }

            
            public string Status { get; set; }

            
            public string Priority { get; set; }

            
            public string? FilePath { get; set; }

           
            public DateTime Deadline { get; set; }

            public List<TaskBoards> TaskBoards { get; set; }


            public List<TaskMembers> Taskmember { get; set; }

        }
    }

