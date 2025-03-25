using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCP.Repositories;
using WCP.Repositories.Models;

namespace WCP.Services
{
    public interface IStyleService
    {
        Task<List<Style>> GetAllAsync();
    }
    public class StyleService : IStyleService
    {
        private readonly StyleRepository _styleRepository;
        public StyleService(StyleRepository styleRepository)
        {
            _styleRepository = styleRepository;
        }
        public async Task<List<Style>> GetAllAsync() => await _styleRepository.GetAllAsync();
    }
}
