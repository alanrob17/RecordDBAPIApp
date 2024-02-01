using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration _config;

        public DataAccess(IConfiguration config)
        {
            _config = config;
        }

        // this method will return a list of type T - only used for queries
        public async Task<IEnumerable<T>> GetDataQ<T, P>(string query, P parameters, string connectionId = "default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            return await connection.QueryAsync<T>(query, parameters);
        }

        // Updated method to support both stored procedures and SQL queries
        public async Task<IEnumerable<T>> GetData<T, P>(string storedProcedure, P parameters, string connectionId = "default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        //This method will not return anything - only used for queries
        public async Task SaveDataQ<P>(string query, P parameters, string connectionId = "default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task SaveData<P>(string storedProcedure, P parameters, string connectionId = "default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> GetCountOrId<P>(string storedProcedure, P parameters, string connectionId = "default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            int result = await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<string> GetText<P>(string storedProcedure, P parameters, string connectionId = "default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            string result = (string)await connection.ExecuteScalarAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}

