using Banka_Renato.Dtos;
using Banka_Renato.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banka_Renato.Data
{
    public interface IAuthRepository
    {
        Task<bool> Register(UserForRegisterDto user);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<bool> EmailExists(string email);

    }
}
