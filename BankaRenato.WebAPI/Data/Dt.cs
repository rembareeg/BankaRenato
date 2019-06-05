using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BankaRenato.WebAPI.Data
{
    public class Dt : DataTable
    {
        
        public SqlCommand Cmd { get; set; } = new SqlCommand();
        private SqlConnection _connection;
        public CommandType Cmdtype = CommandType.StoredProcedure;
        private readonly IConfiguration _config;

        public Dt(IConfiguration config)
        {
            _config = config;
        }

        //Used to open connection
        private async Task<SqlConnection> OpenCn()
        {
            
            Cmd.CommandTimeout = 300;
            if (_connection == null) _connection = new SqlConnection(_config.GetSection("ConnectionStrings:DefaultConnection").Value);
            if (_connection.State ==  System.Data.ConnectionState.Closed || _connection.State == System.Data.ConnectionState.Broken)
            {
                await _connection.OpenAsync();
            }
            return _connection;
        }

        //Used to close connection
        private SqlConnection CloseCn()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }

            return _connection;
        }

        /// <summary>
        /// Used to fetch data
        /// </summary>
        /// <returns></returns>
        public async Task GetData() 
        {
            
            try
            {
                this.Cmd.CommandType = this.Cmdtype;
                await OpenCn().ContinueWith((task) => {
                    Cmd.Connection = task.Result;
                    SqlDataAdapter adap = new SqlDataAdapter(this.Cmd);
                    adap.Fill(this);
                });
                

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                Cmd.Dispose();
                CloseCn();
            }
        }
        
        public async Task ExecuteTaskAsync() 
        {
            try
            {
                this.Cmd.CommandType = this.Cmdtype;
               await OpenCn().ContinueWith((task) => {
                    Cmd.Connection = task.Result;
                });
                await Cmd.ExecuteNonQueryAsync();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                Cmd.Dispose();
                CloseCn();
            }
        }
    }
}



