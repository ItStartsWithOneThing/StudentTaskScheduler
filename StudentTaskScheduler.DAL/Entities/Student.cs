using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.DAL.Entities
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Room { get; set; }
        public string Faculty { get; set; }
        public string Role { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
