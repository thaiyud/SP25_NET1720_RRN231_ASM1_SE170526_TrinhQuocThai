using Microsoft.EntityFrameworkCore;
using SchoolPsychologicalHealthSupportSystem.GenericRepository;
using SchoolPsychologicalHealthSupportSystem.Models;

namespace SchoolPsychologicalHealthSupportSystem
{
    public class UserAccountRepository : GenericRepository<UserAccount>
    {
        public async Task<UserAccount?> GetUserAccount(string username, string password)
        {
            return await _context.UserAccounts.Where(u => u.UserName == username && u.Password == password && u.IsActive == true).FirstOrDefaultAsync();
        }
    }
}
