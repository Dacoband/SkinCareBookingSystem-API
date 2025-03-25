using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WCP.Repositories.Base;
using WCP.Repositories.Models;

namespace WCP.Repositories
{
    public class UserAccountRepository : GenericRepository<UserAccount>
    {
        public UserAccountRepository() { }
        public async Task<UserAccount> GetUserAccount(string userEmail, string userPassword) => await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserEmail == userEmail && u.UserPassword == userPassword);

    }
}
