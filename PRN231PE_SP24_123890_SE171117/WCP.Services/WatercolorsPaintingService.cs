using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCP.Repositories;
using WCP.Repositories.Models;

namespace WCP.Services
{
    public interface IWatercolorsPaintingService
    {
        Task<List<WatercolorsPainting>> GetAllAsync();
        Task<WatercolorsPainting> GetByIdAsync(string id);
        Task<int> CreateAsync(WatercolorsPainting items);
        Task<int> UpdateAsync(WatercolorsPainting items);
        Task<bool> RemoveAsync(string id);
    }
    public class WatercolorsPaintingService : IWatercolorsPaintingService
    {
        private readonly WatercolorsPaintingRepository _watercolorsPaintingRepository;
        public WatercolorsPaintingService(WatercolorsPaintingRepository watercolorsPaintingRepository)
        {
            _watercolorsPaintingRepository = watercolorsPaintingRepository;
        }
        public async Task<List<WatercolorsPainting>> GetAllAsync() => await _watercolorsPaintingRepository.GetAllAsync();
        public async Task<WatercolorsPainting> GetByIdAsync(string id) => await _watercolorsPaintingRepository.GetByIdAsync(id);
        public async Task<int> CreateAsync(WatercolorsPainting items) => await _watercolorsPaintingRepository.CreateAsync(items);
        public async Task<int> UpdateAsync(WatercolorsPainting items) => await _watercolorsPaintingRepository.UpdateAsync(items);
        public async Task<bool> RemoveAsync(string id)
        {
            var item = await _watercolorsPaintingRepository.GetByIdAsync(id);
            if (item != null)
            {
                return await _watercolorsPaintingRepository.RemoveAsync(item);
            }
            return false;

        }
    }
}
