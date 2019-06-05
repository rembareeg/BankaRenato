using BankaRenato.WebAPI.Dtos;
using BankaRenato.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaRenato.WebAPI.Data
{
    public interface IAuthRepository
    {
        Task<bool> Register(UserForRegisterDto user);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<bool> EmailExists(string email);

    }
}
