using SCBS.Repositories;
using SCBS.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBS.Services
{
    public interface IBlogImageService
    {
        Task<int> CreateAsync(BlogImage blogImage);
        Task<bool> DeleteAsync(Guid id);
        Task<List<BlogImage>> GetAllAsync();
        Task<BlogImage> GetByIdAsync(Guid id);
        Task<int> UpdateAsync(BlogImage blogImage);
    }
    public class BlogImageService : IBlogImageService
    {
        private readonly BlogImageRepository _blogImageRepository;
        public BlogImageService() => _blogImageRepository = new BlogImageRepository();
        public async Task<int> CreateAsync(BlogImage blogImage) => await _blogImageRepository.CreateAsync(blogImage);

        public async Task<bool> DeleteAsync(Guid id)
        {
            var blogImage = await _blogImageRepository.GetByIdAsync(id);
            if (blogImage != null)
            {
                return await _blogImageRepository.RemoveAsync(blogImage);
            }
            return false;
        }

        public async Task<List<BlogImage>> GetAllAsync() => await _blogImageRepository.GetAllAsync();

        public async Task<BlogImage> GetByIdAsync(Guid id) => await _blogImageRepository.GetByIdAsync(id);

        public async Task<int> UpdateAsync(BlogImage blogImage) => await _blogImageRepository.UpdateAsync(blogImage);
    }
}
