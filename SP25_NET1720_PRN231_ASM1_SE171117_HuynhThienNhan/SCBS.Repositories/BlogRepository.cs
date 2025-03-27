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
        public async Task<List<Blog>> GetAllAsync() => await _context.Blogs.Include(item => item.BlogImages).ToListAsync();
        public async Task<Blog> GetByIdAsync(Guid id)
        {
            var entity = await _context.Blogs.Include(item => item.BlogImages).FirstOrDefaultAsync(item => item.Id == id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }
        //public async Task<List<Blog>> Search(string? title, string? location, string? status) => await _context.Blogs
        //        .Include(item => item.BlogImages)
        //        .Where(item =>
        //        (string.IsNullOrEmpty(title) || item.Title.Contains(title))
        //                    && (string.IsNullOrEmpty(location) || item.Location.Contains(location))
        //                    && (string.IsNullOrEmpty(status) || item.Status.Contains(status))
        //        )
        //        .ToListAsync();
    }
}
