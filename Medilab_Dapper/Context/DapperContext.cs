using Microsoft.Data.SqlClient;
using System.Data;

namespace Medilab_Dapper.Context
{
    public class DapperContext(IConfiguration configuration)
    {
        private readonly string _connectionString = configuration.GetConnectionString("SqlConnection") 
            ?? throw new ArgumentNullException(nameof(configuration), "Connection string cannot be null.");

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
