using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class EarningType
    {
        
        public int Id { get; set; }
        public string EarningName { get; set; }

        public List<Earning> Earnings { get; set; }
    }
}
