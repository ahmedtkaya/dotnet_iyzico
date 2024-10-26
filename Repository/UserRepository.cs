using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using DOTNET_iyzico.Interfaces;
using DOTNET_iyzico.Data;
using DOTNET_iyzico.Models;
using DOTNET_iyzico.Dtos.User;

namespace DOTNET_iyzico.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _context;

        public UserRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(RegisterUserDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Surname = userDto.Surname,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                IdentityNumber = userDto.IdentityNumber ?? "00000000000",
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password), // Şifre hashleniyor
                AvatarColor = GenerateRandomColor(),
                Address = userDto.Address ?? "Default Address",
                City = userDto.City ?? "Default City",
                Country = userDto.Country ?? "Default Country",
                ZipCode = userDto.ZipCode ?? "00000",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Uuid = Guid.NewGuid(), // Rastgele UUID oluşturuluyor
                Ip = "::1" // Varsayılan IP
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        private string GenerateRandomColor()
        {
            var random = new Random();
            return $"#{random.Next(0x1000000):X6}"; // 6 haneli rastgele hex renk kodu
        }
        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}