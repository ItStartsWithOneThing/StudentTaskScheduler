using StudentTaskScheduler.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.Services.AuthorizationService
{
    public interface IAuthorizationService
    {
        Task<string> SignInAsync(string login, string password);
    }
}
