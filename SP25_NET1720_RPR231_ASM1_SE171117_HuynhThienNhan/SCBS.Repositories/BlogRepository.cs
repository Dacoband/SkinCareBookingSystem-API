using Microsoft.EntityFrameworkCore;
using SCBS.Repositories.Base;
using SCBS.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBS.Repositories
{
    public class BlogRepository : GenericRepository<Blog>
    {
        public BlogRepository() { }
        public new async Task<List<Blog>> GetAllAsync()
        {
            var blogs = await _context.Blogs.Include(b => b.BlogImages).ToListAsync();
            return blogs;
        }
        public new async Task<Blog?> GetByIdAsync(Guid id)
        {
            var blog = await _context.Blogs.Include(b => b.BlogImages).FirstOrDefaultAsync(b => b.Id == id);
            return blog;
        }
        public async Task<List<Blog>> Search(string title, string status)
        {
            var blogs = await _context.Blogs.Include(b => b.BlogImages)
                .Where(b => (string.IsNullOrEmpty(title) || b.Title.Contains(title))
                    && (string.IsNullOrEmpty(status) || b.Status == status))
                .ToListAsync();
            return blogs;
        }
    }
}
