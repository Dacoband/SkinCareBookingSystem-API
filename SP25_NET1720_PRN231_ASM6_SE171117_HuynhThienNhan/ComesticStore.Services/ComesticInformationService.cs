using ComesticStore.Repositories;
using ComesticStore.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComesticStore.Services
{
    public interface IComesticInformationService
    {
        Task<int> CreateAsync(CosmeticInformation item);
        Task<List<CosmeticInformation>> GetAllAsync();
        Task<CosmeticInformation> GetByIdAsync(string id);
        Task<bool> RemoveAsync(string id);
        Task<int> UpdateAsync(CosmeticInformation item);
    }
    public class ComesticInformationService : IComesticInformationService
    {
        private readonly ComesticInformationRepository _comesticInformationRepository;
        public ComesticInformationService() => _comesticInformationRepository = new ComesticInformationRepository();
        public async Task<int> CreateAsync(CosmeticInformation item) => await _comesticInformationRepository.CreateAsync(item);
        public async Task<List<CosmeticInformation>> GetAllAsync() => await _comesticInformationRepository.GetAllAsync();
        public async Task<CosmeticInformation> GetByIdAsync(string id) => await _comesticInformationRepository.GetByIdAsync(id);
        public async Task<bool> RemoveAsync(string id)
        {
            var item = await _comesticInformationRepository.GetByIdAsync(id);
            if (item != null)
            {
                return await _comesticInformationRepository.RemoveAsync(item);
            }
            return false;
        }
        public async Task<int> UpdateAsync(CosmeticInformation item) => await _comesticInformationRepository.UpdateAsync(item);
    }
}
