using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        // שליפת כל המשתמשים
        public async Task<IEnumerable<User>?> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // שליפת משתמש לפי ID
        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        // הוספת משתמש חדש
        public async Task<User> AddAsync(User user)
        {
            if (user == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Email))
            {
                return null;
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        // עדכון משתמש קיים
        public async Task<User> UpdateAsync(Guid id, User obj)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(obj);
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        // מחיקת משתמש מהמסד
        public async Task<int> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return 0;

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }


    }
}