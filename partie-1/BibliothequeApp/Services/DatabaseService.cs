using System;
using MySqlConnector;

namespace BibliothequeApp.Services
{
    public partial class DatabaseService
    {
        private static DatabaseService? _instance;
        private readonly string _connectionString;

        private DatabaseService()
        {
            DotNetEnv.Env.Load();
            var server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
            var database = Environment.GetEnvironmentVariable("DB_DATABASE") ?? "bibliotheque";
            var user = Environment.GetEnvironmentVariable("DB_USER") ?? "biblio_user";
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "biblio_secret";

            _connectionString = $"Server={server};Port={port};Database={database};User ID={user};Password={password};";
        }

        public static DatabaseService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseService();
                }
                return _instance;
            }
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
