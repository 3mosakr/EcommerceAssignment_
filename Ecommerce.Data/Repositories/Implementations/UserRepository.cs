using Ecommerce.DataAccess.Data;
using Ecommerce.DataAccess.Repositories.Interfaces;
using Ecommerce.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _context.Users.ToListAsync();

        public async Task<User?> GetByIdAsync(int id) =>
            await _context.Users.FindAsync(id);

        public async Task<User?> GetByUserNameAsync(string userName) =>
            await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
