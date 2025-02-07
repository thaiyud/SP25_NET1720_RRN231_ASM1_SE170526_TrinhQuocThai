using SchoolPsychologicalHealthSupportSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPsychologicalHealthSupportSystem_Service
{
    public interface IUserAccountService 
    {
        Task<UserAccount> Authenticate(string userName, string password);
    }
}
