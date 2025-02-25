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
        Task<List<Blog>> GetAllAsync();
        Task<Blog?> GetByIdAsync(Guid id);
        Task<int> Create(Blog blog);
        Task<int> Update(Blog blog);
        Task<bool> Delete(Guid id);
        Task<List<Blog>> Search(string title, string status);
    }
    public class BlogService : IBlogService
    {
        private readonly BlogRepository _blogRepository;
        public BlogService()
        {
            _blogRepository = new BlogRepository();
        }
        public async Task<int> Create(Blog blog)
        {
            return await _blogRepository.CreateAsync(blog);
        }
        public async Task<bool> Delete(Guid id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null)
            {
                return false;
            }
            return _blogRepository.Remove(blog);
        }
        public async Task<List<Blog>> GetAllAsync()
        {
            return await _blogRepository.GetAllAsync();
        }
        public async Task<Blog?> GetByIdAsync(Guid id)
        {
            return await _blogRepository.GetByIdAsync(id);
        }
        public async Task<List<Blog>> Search(string title, string status)
        {
            return await _blogRepository.Search(title, status);
        }
        public async Task<int> Update(Blog blog)
        {
            return await _blogRepository.UpdateAsync(blog);
        }
    }
}
