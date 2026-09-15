using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class EventTypes
    {

        [Key]
        public int EventTypesId { get; set; }

        public string Name { get; set; }
        public string Colour { get; set; }
    }
}