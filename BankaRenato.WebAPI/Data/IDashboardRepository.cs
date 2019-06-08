using BankaRenato.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaRenato.WebAPI.Data
{
    public interface IDashboardRepository
    {
        Task<User> GetUser(int id);
        Task<bool> OpenAccount(int id);
        Task<IEnumerable<Account>> GetUserAccounts(int id);
    }
}
