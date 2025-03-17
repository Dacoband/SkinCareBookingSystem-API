using ComesticStore.Repositories.Base;
using ComesticStore.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComesticStore.Repositories
{
    public class ComesticInformationRepository : GenericRepository<CosmeticInformation>
    {
        public ComesticInformationRepository()
        {

        }
        public async Task<List<CosmeticInformation>> GettAllAsync()
        => await _context.CosmeticInformations.Include(item => item.Category).ToListAsync();
        public async Task<CosmeticInformation> GetByIdAsync(string id)
        {
            var entity = await _context.CosmeticInformations.Include(item => item.Category).FirstOrDefaultAsync(item => item.CosmeticId == id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }
    }
}
