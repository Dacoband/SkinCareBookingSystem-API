using ComesticStore.Repositories;
using ComesticStore.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComesticStore.Services
{
    public interface ISystemAccountService
    {
        Task<SystemAccount> Authenticate(string email, string password);
    }
    public class SystemAccountService : ISystemAccountService
    {
        private readonly SystemAccountRepository _systemAccountRepository;
        public SystemAccountService() => _systemAccountRepository = new SystemAccountRepository();
        public async Task<SystemAccount> Authenticate(string email, string password) => await _systemAccountRepository.GetSystemAccount(email, password);
    }
}
