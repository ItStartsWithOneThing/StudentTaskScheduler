using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.DTOs
{
    public class JobFullInfoDTO
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Minimal length is 2")]
        [MaxLength(30, ErrorMessage = "Maximum length is 30")]
        public string Title { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Minimal length is 5")]
        [MaxLength(100, ErrorMessage = "Maximum length is 100")]
        public string Definition { get; set; }

        [Required]
        [DataType(nameof(Guid))]
        public Guid AssignedToId { get; set; }
        public string? AssignedTo { get; set; }
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }
        public string? JobStatus { get; set; }
    }
}
