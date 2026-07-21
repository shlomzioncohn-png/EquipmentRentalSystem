using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class BusinessRepository:IBusinessRepository
    {
        private readonly Context _context;

        public BusinessRepository(Context context)
        {
            _context = context;
        }

        public async Task<Business?> GetByIdAsync(Guid id)
        {
            return await _context.Businesses
                    .Include(b => b.UserOwner) 
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Business> AddAsync(Business business)
        {
            _context.Businesses.Add(business);
            await _context.SaveChangesAsync();
            return business;
        }

        public async Task<Business?> UpdateAsync(Guid id,Business business)
        {
            var entity = await _context.Businesses.FirstOrDefaultAsync(x => x.Id == id); 
            if(entity==null)    
                return null;
            _context.Entry(entity).CurrentValues.SetValues(business);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Business>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Businesses
            .Where(b => b.UserId == userId) 
            .Include(b => b.UserOwner)     
            .ToListAsync();
        }

        public async Task<IEnumerable<Business>?> GetAllAsync()
        {
            return await _context.Businesses
                    .Include(b => b.UserOwner) 
                    .ToListAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var business = await _context.Businesses.FindAsync(id);
            if (business == null) return 0;

            _context.Businesses.Remove(business);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Business>> GetBusinessesByCityAsync(String city)
        {
            return await _context.Businesses.Where(b => b.City == city).ToListAsync();
        }


    }
}
