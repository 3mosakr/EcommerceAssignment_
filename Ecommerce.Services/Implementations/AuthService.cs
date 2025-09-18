using Ecommerce.DataAccess.Data;
using Ecommerce.Entities.Models;
using Ecommerce.Entities.Models.Auth;
using Ecommerce.Services.Interfaces;
using Ecommerce.Services.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly JwtOptions _jwtOptions;
        private readonly IPasswordService _passwordService;

        public AuthService(AppDbContext context, IOptions<JwtOptions> jwtOptions, IPasswordService passwordService)
        {
            _context = context;
            _jwtOptions = jwtOptions.Value;
            _passwordService = passwordService;
        }

        public async Task<AuthResponse?> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null) return null;

            // check password
            if (!_passwordService.VerifyPassword(user.Password, password))
                return null;

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpireDays);

            // update LastLogin
            user.LastLogin = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes)
            };
        }

        public async Task<AuthResponse?> RefreshTokenAsync(string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow) return null;

            var newJwt = GenerateJwtToken(user);
            var newRefresh = GenerateRefreshToken();

            user.RefreshToken = newRefresh;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpireDays);

            await _context.SaveChangesAsync();

            return new AuthResponse
            {
                Token = newJwt,
                RefreshToken = newRefresh,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes)
            };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
