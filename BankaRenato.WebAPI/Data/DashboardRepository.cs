using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BankaRenato.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankaRenato.WebAPI.Data
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly BankaRenatoDBContext _context;

        public DashboardRepository(BankaRenatoDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets user by his id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        public async Task<User> GetUser(int id)
        {
           return await _context.User.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Gets all accounts for user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        public async Task<IEnumerable<Account>> GetUserAccounts(int id)
        {
            SqlParameter sqlUserId = new SqlParameter
            {
                ParameterName = "@userId",
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Value = id
            };

            return await _context.Account.FromSql("EXECUTE GetUserAccounts @userId", sqlUserId).ToListAsync();
        }
        /// <summary>
        /// Opens account for user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        public async Task<bool> OpenAccount(int id)
        {
            SqlParameter sqlUserId = new SqlParameter
            {
                ParameterName = "@userId",
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Value = id
            };
            SqlParameter sqlResponse = new SqlParameter
            {
                ParameterName = "@response",
                DbType = DbType.Boolean,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlCommandAsync("EXECUTE OpenAccount @userId, @response OUT", sqlUserId, sqlResponse);

            return (bool)sqlResponse.Value;
        }
        /// <summary>
        /// Gets user by his id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        public async Task<Account> GetUserAccount(int id)
        {
            return await _context.Account.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Creating card for account
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="cardType">Card type</param>
        /// <returns></returns>
        public async Task<bool> CreateCard(int accountId, int cardType)
        {
            SqlParameter sqlAccount = new SqlParameter
            {
                ParameterName = "@accountId",
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Value = accountId
            };
            SqlParameter sqlCard = new SqlParameter
            {
                ParameterName = "@cardId",
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Value = cardType
            };
            SqlParameter sqlResponse = new SqlParameter
            {
                ParameterName = "@response",
                DbType = DbType.Boolean,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlCommandAsync("EXECUTE CreateCard @accountId, @cardId, @response OUT", sqlAccount, sqlCard, sqlResponse);

            return (bool)sqlResponse.Value;
        }

        public async Task<IEnumerable<CardType>> GetCardTypes()
        {
            return await _context.CardType.ToListAsync();
        }
        /// <summary>
        /// Returns list of cards for account
        /// </summary>
        /// <param name="id">Account id</param>
        /// <returns></returns>
        public async Task<IEnumerable<Card>> GetAccountCards(int id)
        {
            return await _context.Card.Where(cards => cards.AccountId == id).ToListAsync();
        }
    }
}
