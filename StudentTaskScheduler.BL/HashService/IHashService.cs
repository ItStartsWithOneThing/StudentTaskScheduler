using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.HashService
{
    public interface IHashService
    {
        string HashString(string source);
    }
}
