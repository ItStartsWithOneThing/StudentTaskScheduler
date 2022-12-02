using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.DTOs
{
    public class StudentFullInfoDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Room { get; set; }
        public string Faculty { get; set; }
        public string? Role { get; set; }
        public ICollection<JobDTO> Jobs { get; set; }
    }
}
