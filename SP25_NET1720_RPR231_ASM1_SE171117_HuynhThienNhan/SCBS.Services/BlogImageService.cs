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
        Task<List<BlogImage>> GetAllAsync();
        Task<BlogImage?> GetByIdAsync(Guid id);
        Task<int> Create(BlogImage blogImage);
        Task<int> Update(BlogImage blogImage);
        Task<bool> Delete(Guid id);
        Task<List<Blog>> Search(Guid blogId);
    }
    public class BlogImageService
    {
        private readonly BlogImageRepository _blogImageRepository;
        public BlogImageService()
        {
            _blogImageRepository = new BlogImageRepository();
        }
        public async Task<int> Create(BlogImage blogImage)
        {
            return await _blogImageRepository.CreateAsync(blogImage);
        }
        public async Task<bool> Delete(Guid id)
        {
            var blogImage = await _blogImageRepository.GetByIdAsync(id);
            if (blogImage == null)
            {
                return false;
            }
            return _blogImageRepository.Remove(blogImage);
        }
        public async Task<List<BlogImage>> GetAllAsync()
        {
            return await _blogImageRepository.GetAllAsync();
        }
        public async Task<BlogImage?> GetByIdAsync(Guid id)
        {
            return await _blogImageRepository.GetByIdAsync(id);
        }
        public async Task<List<BlogImage>> Search(Guid blogId)
        {
            return await _blogImageRepository.Search(blogId);
        }
        public async Task<int> Update(BlogImage blogImage)
        {
            return await _blogImageRepository.UpdateAsync(blogImage);
        }
    }
}
