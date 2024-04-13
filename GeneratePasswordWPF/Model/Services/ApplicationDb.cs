
using GeneratePasswordWPF.Model.DbTables;
using Microsoft.Data.Sqlite;

namespace GeneratePasswordWPF.Model.Services
{
    public class ApplicationDb
    {
        public ApplicationDb()
        {
            var connection = new SqliteConnection("Data Source=ApplicationDb.db");
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            try
            {
                command.Connection = connection;
                command.CommandText = "CREATE TABLE UserTable(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Login TEXT NOT NULL, Password TEXT NOT NULL)";
                command.ExecuteNonQuery();
            }
            catch { }
        }
        private SqliteConnection Conn()
        {
            var connection = new SqliteConnection("Data Source=ApplicationDb.db");
            connection.Open();
            return connection;
        }

        public void AddAcc(string Login, string Password)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = Conn();
            command.CommandText = $"INSERT INTO UserTable (Login,Password) VALUES ('{Login}','{Password}')";
            command.ExecuteNonQuery();
        }
    }
}
