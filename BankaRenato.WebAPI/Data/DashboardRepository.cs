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
        public async Task<User> GetUser(int id)
        {
           return await _context.User.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

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

        public async Task<bool> OpenAccount(int id)
        {
            SqlParameter sqlUserId = new SqlParameter
            {
                ParameterName = "@userdId",
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

            await _context.Database.ExecuteSqlCommandAsync("EXECUTE OpenAccount @userdId, @response OUT", sqlUserId, sqlResponse);

            return (bool)sqlResponse.Value;
        }
    }
}
