using ComesticStore.Repositories;
using ComesticStore.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComesticStore.Services
{
    public interface IComesticCategoryService
    {
        Task<List<CosmeticCategory>> GetAllAsync();
    }
    public class ComesticCategoryService : IComesticCategoryService
    {
        private readonly ComesticCategoryRepository _comesticCategoryRepository;
        public ComesticCategoryService() => _comesticCategoryRepository = new ComesticCategoryRepository();
        public async Task<List<CosmeticCategory>> GetAllAsync() => await _comesticCategoryRepository.GetAllAsync();
    }
}
