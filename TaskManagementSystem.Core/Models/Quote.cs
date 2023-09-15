using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Core.Models
{
    public class Quote
    {
        public int QuoteID { get; set; }
        public string QuoteType { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Premium { get; set; }
        public string Sales { get; set; }
    }
}
