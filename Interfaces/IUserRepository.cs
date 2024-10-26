using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOTNET_iyzico.Models;
using DOTNET_iyzico.Dtos.User;

namespace DOTNET_iyzico.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(RegisterUserDto userDto);
        Task<bool> IsEmailRegisteredAsync(string email);
        Task<User> GetUserByEmailAsync(string email);

    }
}