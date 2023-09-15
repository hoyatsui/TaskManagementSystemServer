using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Core.DTOs
{
    public class QuoteDTO
    {
        public int QuoteID { get; set; }
        public string QuoteType { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string FormattedDueDate => DueDate.ToString("MM/dd/yyyy");
        public string Premium { get; set; }
        public string Sales { get; set; }
    }
}
