﻿using Microsoft.Data.Sqlite;

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
                command.CommandText = "CREATE TABLE UserTable(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Login TEXT NOT NULL, Password TEXT NOT NULL, SocietyId INTEGET , FOREIGN KEY (SocietyId) REFERENCES SocietyTable(SocietyId))";
                command.ExecuteNonQuery();
            }
            catch { }

            try
            {
                command.Connection = connection;
                command.CommandText = "CREATE TABLE SocietyTable (SocietyId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, SocietyName TEXT NOT NULL,Count INTEGET, Description TEXT)";
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
            SqliteCommand command =new SqliteCommand();
            command.Connection=Conn();
            command.CommandText = $"INSERT INTO SocietyTable (SocietyName,Description) VALUES ('{SocietyName}','{Description}')";
            command.ExecuteNonQuery();
        }

        public List<string> SelectSociety()
        {
            List<string> societies = new List<string>();

            SqliteCommand command = new SqliteCommand();
            command.Connection = Conn();
            command.CommandText = $"SELECT SocietyName,Description FROM SocietyTable";

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                string societyName = reader["SocietyName"].ToString();
                string description = reader["Description"].ToString();
                societies.Add(societyName);
            }

            reader.Close();
            command.Connection.Close();
            return societies;
        }

        public int GetSocietyId(string societyName)
        {
            int societyId = -1; // Или какое-то значение по умолчанию, если соц-сеть не найдена

            using (SqliteCommand command = new SqliteCommand())
            {
                command.Connection = Conn();
                command.CommandText = $"SELECT SocietyId FROM SocietyTable WHERE SocietyName = @SocietyName";
                command.Parameters.AddWithValue("@SocietyName", societyName);

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    societyId = Convert.ToInt32(result);
                }
            }

            return societyId;
        }
    }
}
