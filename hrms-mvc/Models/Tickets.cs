using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class Tickets
    {
        [Key]
        public int TicketId { get; set; }

        [Required]
        [StringLength(20)]
        public string? TicketNo { get; set; }

        [Required]
        [StringLength(100)]
        public string? Subject { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        [StringLength(20)]
        public string? Priority { get; set; }

        [Required]
        public int RaisedBy { get; set; }
        public int? AssignedTo { get; set; }
        public int? AssignedBy { get; set; }


        [Required]
        [StringLength(30)]
        public string? Status { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? AssignedDate { get; set; }

        public DateTime? StartedDate { get; set; }

        public DateTime? ResolvedDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        
        public User? RaisedByUser { get; set; }

        public User? AssignedToUser { get; set; }

        public User? AssignedByUser { get; set; }

        public ICollection<TicketComment>? Comments { get; set; }

        public TicketResolution? Resolution { get; set; }

        public ICollection<TicketAttachment>? Attachments { get; set; }

    }
}
