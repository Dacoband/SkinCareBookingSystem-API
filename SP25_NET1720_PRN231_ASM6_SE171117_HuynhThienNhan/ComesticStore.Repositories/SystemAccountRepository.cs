using ComesticStore.Repositories.Base;
using ComesticStore.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComesticStore.Repositories
{
    public class SystemAccountRepository : GenericRepository<SystemAccount>
    {
        public SystemAccountRepository()
        {

        }
        public async Task<SystemAccount> GetSystemAccount(string email, string password) => await _context.SystemAccounts.FirstOrDefaultAsync(u => u.EmailAddress == email && u.AccountPassword == password);
    }
}
