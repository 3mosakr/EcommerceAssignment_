using Ecommerce.DataAccess.Repositories.Interfaces;
using Ecommerce.Entities.Models;
using Ecommerce.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _repo.GetAllAsync();

        public async Task<User?> GetUserByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<User?> GetUserByUserNameAsync(string userName) => await _repo.GetByUserNameAsync(userName);

        public async Task<User?> RegisterUserAsync(User user)
        {
            await _repo.AddAsync(user);
            return user;
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            await _repo.UpdateAsync(user);
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;
            await _repo.DeleteAsync(user);
            return true;
        }
    }
}
