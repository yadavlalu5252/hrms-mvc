using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class EventModel
    {

        
        public int EventModelId { get; set; }
        
        public string Titles { get; set; }
        
        public string Date { get; set; }
        public string Status { get; set; }
        public int EventTypeId { get; set; }
    }
}
