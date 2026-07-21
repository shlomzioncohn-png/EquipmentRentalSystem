using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly Context _context;

        public ItemRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>?> GetAllAsync()
        {
            return await _context.Item
                    .Include(v => v.AssociatedBusiness) 
                    .ToListAsync();
        }

        public async Task<Item?> GetByIdAsync(Guid id)
        {
            return await _context.Item
                    .Include(v => v.AssociatedBusiness) 
                    .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Item> AddAsync(Item item)
        {
            if (item == null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(item.Description))
            {
                return null;
            }
            if (item.Price <0 || item.Name==null)
            {
                return null;
            }
            _context.Item.Add(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<Item> UpdateAsync(Guid id, Item obj)
        {
            var entity = await _context.Item.FirstOrDefaultAsync(v => v.Id == id);

            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(obj);
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var item = await _context.Item.FindAsync(id);

            if (item == null)
            {
                return 0; 
            }

            _context.Item.Remove(item);
            return await _context.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<Item>> GetByBusinessIdAsync(Guid businessId)
        {
            return await _context.Item
         .Where(vt => vt.BusinessId == businessId)
         .Include(vt => vt.AssociatedBusiness) 
         .ToListAsync();
        }
    }
}