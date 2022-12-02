using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.DTOs
{
    public class JobDTO
    {
        public string Title { get; set; }
        public string Definition { get; set; }
        public string AssignedTo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Status { get; set; }
    }
}
