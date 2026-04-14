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
            _connectionString = "Server=localhost;Port=3306;Database=bibliotheque;User ID=biblio_user;Password=biblio_secret;";
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
