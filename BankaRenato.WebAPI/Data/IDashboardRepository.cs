using BankaRenato.WebAPI.Dtos;
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
        Task<IEnumerable<User>> GetUsersByRole(string role);
        Task<bool> OpenAccount(int id, string name);
        Task<IEnumerable<Account>> GetUserAccounts(int id);
        Task<Account> GetUserAccount(int id);
        Task<bool> CreateCard(int accountId, int cardType);
        Task<IEnumerable<CardType>> GetCardTypes();
        Task<IEnumerable<CardForDashboardDto>> GetAccountCards(int id);
        Task<bool> DeleteUser(int id);
        Task<bool> DeleteAccount(int id);
        Task<bool> DeleteCard(int id);
        Task<bool> UpdateCard(int id, int cardType);
        Task<bool> UpdateUserAsAdmin(UserForUpdateAsAdminDto user);
        Task<bool> UpdateUserAsUser(UserForUpdateAsUserDto user);
        Task<bool> UpdateAccount(AccountForUpdateDto account);
        Task<bool> UserOwnsAccount(int userId, int accountId);



    }
}
