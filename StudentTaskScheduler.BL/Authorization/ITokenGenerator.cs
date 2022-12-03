using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.Authorization
{
    public interface ITokenGenerator
    {
        string GenerateToken(string userName, string role);
    }
}
