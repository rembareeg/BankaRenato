using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BankaRenato.WebAPI.Dtos;
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

        public async Task<IEnumerable<User>> GetUsersByRole(string role)
        {
            return await _context.User.Where(users => users.Role == role).ToListAsync();
        }

        public async Task<bool> DeleteUser(int id)
        {
            SqlParameter sqlUserId = new SqlParameter
            {
                ParameterName = "@userId",
                DbType = DbType.Int32,
                Direction = ParameterDirection.Input,
                Value = id
            };
            SqlParameter sqlResponse = new SqlParameter
            {
                ParameterName = "@response",
                DbType = DbType.Boolean,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlCommandAsync("EXECUTE DeleteUser @userId, @response OUT", sqlUserId, sqlResponse);

            return (bool)sqlResponse.Value;
        }

        public async Task<bool> DeleteAccount(int id)
        {
            SqlParameter sqlAccountId = new SqlParameter
            {
                ParameterName = "@accountId",
                DbType = DbType.Int32,
                Direction = ParameterDirection.Input,
                Value = id
            };
            SqlParameter sqlResponse = new SqlParameter
            {
                ParameterName = "@response",
                DbType = DbType.Boolean,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlCommandAsync("EXECUTE DeleteAccount @accountId, @response OUT", sqlAccountId, sqlResponse);

            return (bool)sqlResponse.Value;
        }

        public async Task<bool> DeleteCard(int id)
        {
            SqlParameter sqlCardId = new SqlParameter
            {
                ParameterName = "@cardId",
                DbType = DbType.Int32,
                Direction = ParameterDirection.Input,
                Value = id
            };
            SqlParameter sqlResponse = new SqlParameter
            {
                ParameterName = "@response",
                DbType = DbType.Boolean,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlCommandAsync("EXECUTE DeleteCard @cardId, @response OUT", sqlCardId, sqlResponse);

            return (bool)sqlResponse.Value;
        }

        public async Task<bool> UpdateCard(int id, int cardType)
        {
            SqlParameter sqlCardId = new SqlParameter
            {
                ParameterName = "@cardId",
                DbType = DbType.Int32,
                Direction = ParameterDirection.Input,
                Value = id
            };
            SqlParameter sqlCardType = new SqlParameter
            {
                ParameterName = "@cardType",
                DbType = DbType.Int16,
                Direction = ParameterDirection.Input,
                Value = cardType
            };
            SqlParameter sqlResponse = new SqlParameter
            {
                ParameterName = "@response",
                DbType = DbType.Boolean,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlCommandAsync("EXECUTE UpdateCard @cardId, @cardType, @response OUT", sqlCardId, sqlCardType, sqlResponse);

            return (bool)sqlResponse.Value;
        }

        public async Task<bool> UpdateUser(UserForUpdateDto user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter{ParameterName = "@userId", DbType = DbType.Int32, Direction = ParameterDirection.Input, Value = user.Id},
                new SqlParameter{ParameterName = "@username", DbType = DbType.String, Direction = ParameterDirection.Input, Value = user.Username.ToLower()},
                new SqlParameter{ParameterName = "@password", DbType = DbType.String, Direction = ParameterDirection.Input, Value = user.Password},
                new SqlParameter{ParameterName = "@email", DbType = DbType.String, Direction = ParameterDirection.Input, Value = user.Email.ToLower()},
                new SqlParameter{ParameterName = "@firstName", DbType = DbType.String, Direction = ParameterDirection.Input, Value = user.FirstName},
                new SqlParameter{ParameterName = "@lastName ", DbType = DbType.String, Direction = ParameterDirection.Input, Value = user.LastName},
                new SqlParameter{ParameterName = "@response", DbType = DbType.Boolean,Direction = ParameterDirection.Output}
            };

            await _context.Database.ExecuteSqlCommandAsync("EXECUTE UpdateUser @userId, @username, @password, @email, @firstName, @lastName, @response OUT", parameters);

            return (bool)parameters[parameters.Length - 1].Value;
        }
    }
}
