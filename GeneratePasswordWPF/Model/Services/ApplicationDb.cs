using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Windows.Controls;

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
                command.CommandText = "CREATE TABLE IF NOT EXISTS UserTable(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Login TEXT NOT NULL, Password TEXT NOT NULL, SocietyId INTEGER, FOREIGN KEY (SocietyId) REFERENCES SocietyTable(SocietyId))";
                command.ExecuteNonQuery();
            }
            catch { }

            try
            {
                command.Connection = connection;
                command.CommandText = "CREATE TABLE IF NOT EXISTS SocietyTable (SocietyId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, SocietyName TEXT NOT NULL, Description TEXT)";
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

        public void AddAcc(string Login, string Password, int SocietyId)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = Conn();
            command.CommandText = $"INSERT INTO UserTable (Login,Password,SocietyId) VALUES ('{Login}','{Password}','{SocietyId}')";
            command.ExecuteNonQuery();
        }

        public void AddSociety(string SocietyName, string Description)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = Conn();
            command.CommandText = $"INSERT INTO SocietyTable (SocietyName,Description) VALUES ('{SocietyName}','{Description}')";
            command.ExecuteNonQuery();
        }

        public class UserInfo
        {
            public int Id { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string SocietyName { get; set; }
            public string Description { get; set; }
        }

        public List<UserInfo> SelectInfoUser()
        {
            List<UserInfo> listSelectInfoUser = new List<UserInfo>();
            SqliteCommand command = new SqliteCommand();
            command.Connection = Conn();
            command.CommandText = $"SELECT u.Id, u.Login, u.Password, s.SocietyName, s.Description FROM UserTable u JOIN SocietyTable s ON u.SocietyId = s.SocietyId";
            SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                UserInfo userInfo = new UserInfo
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Login = reader.GetString(reader.GetOrdinal("Login")),
                    Password = reader.GetString(reader.GetOrdinal("Password")),
                    SocietyName = reader.GetString(reader.GetOrdinal("SocietyName")),
                    Description = reader.GetString(reader.GetOrdinal("Description"))
                };
                listSelectInfoUser.Add(userInfo);
            }
            return listSelectInfoUser;
        }


        public void CreateUserInfoTable()
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = Conn();

            // Создание таблицы UserInfo с данными из UserTable и SocietyTable
            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS UserInfo AS
            SELECT u.Id, u.Login, u.Password, s.SocietyName, s.Description
            FROM UserTable u
            JOIN SocietyTable s ON u.SocietyId = s.SocietyId;
        ";

            command.ExecuteNonQuery();
        }

        public void DelInfoUser(int Id)
        {
            CreateUserInfoTable();
            SqliteCommand command = new SqliteCommand();
            command.Connection = Conn();
            command.CommandText = $"DELETE FROM UserTable WHERE Id = {Id}";
            command.ExecuteNonQuery();
        }

        public void UpdateSociety(string newName, string newDescription, int societyId)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = Conn();
            command.CommandText = $"UPDATE SocietyTable SET SocietyName = @Name, Description = @Description WHERE SocietyId = @SocietyId";
            command.Parameters.AddWithValue("@Name", newName);
            command.Parameters.AddWithValue("@Description", newDescription);
            command.Parameters.AddWithValue("@SocietyId", societyId);
            command.ExecuteNonQuery();
        }

        public List<string> SelectSociety(out List<string> societiesDesc)
        {
            List<string> societies = new List<string>();
            societiesDesc = new List<string>();
            SqliteCommand command = new SqliteCommand();
            command.Connection = Conn();
            command.CommandText = $"SELECT SocietyId, SocietyName,Description FROM SocietyTable";

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string societyName = reader["SocietyName"].ToString();
                string description = reader["Description"].ToString();

                societies.Add(societyName);
                societiesDesc.Add(description);
            }

            reader.Close();
            command.Connection.Close();
            return societies;
        }

        public int GetSocietyId(string societyName)
        {
            int societyId = -1; // Или какое-то значение по умолчанию, если соц-сеть не найдена
            SqliteCommand command = new SqliteCommand();
            command.Connection = Conn();
            command.CommandText = $"SELECT SocietyId FROM SocietyTable WHERE SocietyName = @SocietyName";
            command.Parameters.AddWithValue("@SocietyName", societyName);

            object result = command.ExecuteScalar();
            if (result != null)
            {
                societyId = Convert.ToInt32(result);
            }


            return societyId;
        }
    }
}
