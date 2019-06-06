using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BankaRenato.WebAPI.Dtos;
using BankaRenato.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BankaRenato.WebAPI.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly BankaRenatoDBContext _context;

        public AuthRepository(BankaRenatoDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// If user is successfully logged in it will return object otherwise it will return null
        /// </summary>
        /// <param name="username">User username</param>
        /// <param name="password">User password</param>
        /// <returns>User object</returns>
       public async Task<User> Login(string username, string password)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter{ParameterName = "@username", DbType = DbType.String,Direction = ParameterDirection.Input,Value = username.ToLower()},
                new SqlParameter{ParameterName = "@password", DbType = DbType.String,Direction = ParameterDirection.Input,Value = password}
                };

            User user = await _context.User.FromSql("EXECUTE UserLogin @username, @password", parameters).FirstOrDefaultAsync() as User;

            return user;
        }
        /// <summary>
        /// Returns true if user is successfully registrated
        /// </summary>
        /// <param name="user">UserForRegisterDto object</param>
        /// <returns></returns>
        public async Task<bool> Register(UserForRegisterDto user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter{ParameterName = "@username", DbType = DbType.String, Direction = ParameterDirection.Input, Value = user.Username.ToLower()},
                new SqlParameter{ParameterName = "@password", DbType = DbType.String, Direction = ParameterDirection.Input, Value = user.Password},
                new SqlParameter{ParameterName = "@email", DbType = DbType.String, Direction = ParameterDirection.Input, Value = user.Email.ToLower()},
                new SqlParameter{ParameterName = "@firstName", DbType = DbType.String, Direction = ParameterDirection.Input, Value = user.FirstName},
                new SqlParameter{ParameterName = "@lastName ", DbType = DbType.String, Direction = ParameterDirection.Input, Value = user.LastName},
                new SqlParameter{ParameterName = "@response",DbType = DbType.Boolean,Direction = ParameterDirection.Output}
        };

            await _context.Database.ExecuteSqlCommandAsync("EXECUTE RegisterUser @username, @password, @email, @firstName, @lastName, @response OUT", parameters);

            return (bool)parameters[parameters.Length - 1].Value;
        }        
        /// <summary>
        /// Returns true if user with username exists
        /// </summary>
        /// <param name="username">User username</param>
        /// <returns></returns>
        public async Task<bool> UserExists(string username)
        {
            SqlParameter sqlUsername = new SqlParameter
            {
                ParameterName = "@username",
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Value = username.ToLower()
            };
            SqlParameter sqlResponse = new SqlParameter
            {
                ParameterName = "@response",
                DbType = DbType.Boolean,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlCommandAsync("EXECUTE UserExists @username, @response OUT", sqlUsername, sqlResponse);

            return (bool)sqlResponse.Value;
        }
        /// <summary>
        /// Returns true if user with email exists
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns></returns>
        public async Task<bool> EmailExists(string email)
        {
            SqlParameter sqlEmail = new SqlParameter
            {
                ParameterName = "@email",
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Value = email.ToLower()
            };
            SqlParameter sqlResponse = new SqlParameter
            {
                ParameterName = "@response",
                DbType = DbType.Boolean,
                Direction = ParameterDirection.Output
            };
            
            await _context.Database.ExecuteSqlCommandAsync("EXECUTE EmailExists @email, @response OUT", sqlEmail, sqlResponse);

            return (bool)sqlResponse.Value;
        }
    }
}
