using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.DTOs
{
    public class StudentFullInfoDTO
    {
        public Guid? Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Minimal length is 2")]
        [MaxLength(25, ErrorMessage = "Maximum length is 25")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Minimal length is 2")]
        [MaxLength(25, ErrorMessage = "Maximum length is 25")]
        public string LastName { get; set; }

        [Required]
        [Range(18, 30, ErrorMessage = "Age must be between 18 and 30 years")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Student should have a room")]
        [Range(1,7, ErrorMessage = "Our block has room numbers only from 1 to 7")]
        public int Room { get; set; }
        public string Faculty { get; set; }
        public string Role { get; set; }
        public ICollection<JobDTO> Jobs { get; set; }
    }
}
