using Ecommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUserNameAsync(string userName);
        Task<User?> RegisterUserAsync(User user);
        Task<User?> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
