using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCP.Repositories.Base;
using WCP.Repositories.Models;

namespace WCP.Repositories
{
    public class WatercolorsPaintingRepository : GenericRepository<WatercolorsPainting>
    {
        public WatercolorsPaintingRepository() { }
        public new async Task<List<WatercolorsPainting>> GetAllAsync()
            => await _context.WatercolorsPaintings.Include(w => w.Style).ToListAsync();

        public new async Task<WatercolorsPainting> GetByIdAsync(string id)
        {
            var entity = await _context.WatercolorsPaintings.Include(item => item.Style).FirstOrDefaultAsync(item => item.StyleId == id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

    }
}
