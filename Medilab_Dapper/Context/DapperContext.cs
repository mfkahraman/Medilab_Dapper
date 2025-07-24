using Microsoft.Data.SqlClient;
using System.Data;

namespace Medilab_Dapper.Context
{
    public class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConnection") 
                ?? throw new ArgumentNullException(nameof(configuration), "Connection string cannot be null.");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
