using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Banka_Renato.Dtos;
using Banka_Renato.Models;
using Microsoft.Extensions.Configuration;

namespace Banka_Renato.Data
{
    public class AuthRepository : IAuthRepository
    {

        private readonly IConfiguration _config;

        public AuthRepository(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// If user is successfully logged in it will return object otherwise it will return null
        /// </summary>
        /// <param name="username">User username</param>
        /// <param name="password">User password</param>
        /// <returns>User object</returns>
        public async Task<User> Login(string username, string password)
        {
            Dt database = new Dt(_config);
            database.Cmd.CommandText = "UserLogin";
            database.Cmd.Parameters.AddWithValue("@username", username.ToLower()).Direction = ParameterDirection.Input;
            database.Cmd.Parameters.AddWithValue("@password", password).Direction = ParameterDirection.Input;
            
            database.Cmd.Parameters.Add("@response", SqlDbType.Int).Direction = ParameterDirection.Output;
            await database.GetData();

            if(database.Rows.Count == 1)
            {
                User user = new User();
                user.Id = (int)database.Rows[0][0];
                user.Username = (string)database.Rows[0][1];
                user.Email = (string)database.Rows[0][2];
                user.FirstName = (string)database.Rows[0][3];
                user.LastName = (string)database.Rows[0][4];

                return user;
            }

            return null;
            
        }
        /// <summary>
        /// Returns true if user is successfully registrated
        /// </summary>
        /// <param name="user">UserForRegisterDto object</param>
        /// <returns></returns>
        public async Task<bool> Register(UserForRegisterDto user)
        {
            Dt database = new Dt(_config);
            database.Cmd.CommandText = "RegisterUser";
            database.Cmd.Parameters.AddWithValue("@username", user.Username.ToLower()).Direction = ParameterDirection.Input;
            database.Cmd.Parameters.AddWithValue("@password", user.Password).Direction = ParameterDirection.Input;
            database.Cmd.Parameters.AddWithValue("@email", user.Email.ToLower()).Direction = ParameterDirection.Input;
            database.Cmd.Parameters.AddWithValue("@firstName", user.FirstName).Direction = ParameterDirection.Input;
            database.Cmd.Parameters.AddWithValue("@lastName", user.LastName).Direction = ParameterDirection.Input;
            database.Cmd.Parameters.Add("@response", SqlDbType.Bit).Direction = ParameterDirection.Output;
            await database.GetData();
            return (bool)(database.Cmd.Parameters["@response"].Value);

        }        
        /// <summary>
        /// Returns true if user with username exists
        /// </summary>
        /// <param name="username">User username</param>
        /// <returns></returns>
        public async Task<bool> UserExists(string username)
        {
            Dt database = new Dt(_config);
            database.Cmd.CommandText = "UserExists";
            database.Cmd.Parameters.AddWithValue("@username", username.ToLower()).Direction = ParameterDirection.Input;
            database.Cmd.Parameters.Add("@response", SqlDbType.Bit).Direction = ParameterDirection.Output;
            await database.GetData();
            return (bool)(database.Cmd.Parameters["@response"].Value);
        }
        /// <summary>
        /// Returns true if user with email exists
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns></returns>
        public async Task<bool> EmailExists(string email)
        {
            Dt database = new Dt(_config);
            database.Cmd.CommandText = "EmailExists";
            database.Cmd.Parameters.AddWithValue("@email", email.ToLower()).Direction = ParameterDirection.Input;
            database.Cmd.Parameters.Add("@response", SqlDbType.Bit).Direction = ParameterDirection.Output;
            await database.GetData();
            return (bool)(database.Cmd.Parameters["@response"].Value);
        }
    }
}
