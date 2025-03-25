using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCP.Repositories;
using WCP.Repositories.Models;

namespace WCP.Services
{
    public interface IUserAccountService
    {
        Task<UserAccount> Authenticate(string userEmail, string userPassword);
    }
    public class UserAccountService : IUserAccountService
    {
        public UserAccountService(UserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }
        private readonly UserAccountRepository _userAccountRepository;
        public async Task<UserAccount> Authenticate(string userEmail, string userPassword) => await _userAccountRepository.GetUserAccount(userEmail, userPassword);
    }
}
