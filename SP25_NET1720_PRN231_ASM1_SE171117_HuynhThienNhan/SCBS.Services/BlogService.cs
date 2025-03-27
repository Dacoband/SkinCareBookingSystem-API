using SCBS.Repositories;
using SCBS.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBS.Services
{
    public interface IBlogService
    {
        Task<int> CreateAsync(Blog item);
        Task<List<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(Guid id);
        Task<bool> RemoveAsync(Guid id);
        //Task<List<Blog>> Search(string? title, string? location, string? status);
        Task<int> UpdateAsync(Blog item);
    }
    public class BlogService : IBlogService
    {
        private readonly BlogRepository _blogRepository;
        public BlogService() => _blogRepository = new BlogRepository();
        public async Task<int> CreateAsync(Blog item) => await _blogRepository.CreateAsync(item);

        public async Task<bool> RemoveAsync(Guid id)
        {
            var item = await _blogRepository.GetByIdAsync(id);
            if (item != null)
            {
                return await _blogRepository.RemoveAsync(item);
            }
            return false;
        }

        public async Task<List<Blog>> GetAllAsync() => await _blogRepository.GetAllAsync();

        public async Task<Blog> GetByIdAsync(Guid id) => await _blogRepository.GetByIdAsync(id);

        //public async Task<List<Blog>> Search(string? title, string? location, string? status) => await _blogRepository.Search(title, location, status);

        public async Task<int> UpdateAsync(Blog item) => await _blogRepository.UpdateAsync(item);
    }
}
