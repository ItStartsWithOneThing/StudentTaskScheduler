using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.DAL.Entities
{
    public class Job : BaseEntity
    {
        public string Title { get; set; }
        public string Definition { get; set; }
        public Guid AssignedToId { get; set; }
        public Student AssignedTo { get; set; }        
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Status { get; set; }
    }
}
