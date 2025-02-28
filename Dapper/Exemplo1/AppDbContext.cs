using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace Exemplo1
{
    public class AppDbContext
    {
        private static readonly string connectionString = "Host=localhost;Database=digix-sql;Username=postgres;Password=olszewski";

        public static IDbConnection GetConexao()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}