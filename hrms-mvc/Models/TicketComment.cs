using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class TicketComment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public int TicketId { get; set; }

        [Required]
        public int CommentBy { get; set; }

        [Required]
        public string? CommentText { get; set; }

        [Required]
        public DateTime CommentDate { get; set; }


        
        public Tickets? Ticket { get; set; }

        public User? CommentByUser { get; set; }
    }
}
