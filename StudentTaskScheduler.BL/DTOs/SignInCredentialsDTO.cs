using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.DTOs
{
    public class SignInCredentialsDTO
    {
        [Required]
        [MinLength(2, ErrorMessage = "Minimal length is 2")]
        [MaxLength(15, ErrorMessage = "Maximum length is 15")]
        public string Login { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Minimal length is 2")]
        [MaxLength(12, ErrorMessage = "Maximum length is 12")]
        public string Password { get; set; }
    }
}
