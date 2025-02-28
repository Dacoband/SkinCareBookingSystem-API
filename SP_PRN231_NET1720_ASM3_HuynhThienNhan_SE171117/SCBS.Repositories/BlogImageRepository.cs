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
    public class BlogImageRepository : GenericRepository<BlogImage>
    {
        public BlogImageRepository() { }
        public new async Task<List<BlogImage>> GetAllAsync()
        {
            var blogImages = await _context.BlogImages.Include(b => b.Blog).ToListAsync();
            return blogImages;
        }
        public new async Task<BlogImage?> GetByIdAsync(Guid id)
        {
            var blogImage = await _context.BlogImages.Include(b => b.Blog).FirstOrDefaultAsync(b => b.Id == id);
            return blogImage;
        }
        public async Task<List<BlogImage>> Search(Guid blogId)
        {
            var blogImages = await _context.BlogImages.Include(b => b.Blog)
                .Where(b => b.BlogId == blogId)
                .ToListAsync();
            return blogImages;
        }
    }
}
