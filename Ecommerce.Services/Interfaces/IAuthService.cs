using Ecommerce.Entities.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse?> AuthenticateAsync(string username, string password);
        Task<AuthResponse?> RefreshTokenAsync(string refreshToken);
    }
}
