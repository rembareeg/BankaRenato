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
        Task<Account> GetUserAccount(int id);
        Task<bool> CreateCard(int accountId, int cardType);
        Task<IEnumerable<CardType>> GetCardTypes();
        Task<IEnumerable<Card>> GetAccountCards(int id);
        
    }
}
