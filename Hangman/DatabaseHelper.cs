using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml.Linq;

namespace Hangman
{
    internal class DatabaseHelper
    {
        private static string connectionStringDatabase = @"Data Source=..\..\Database\database.db;Version=3;";
        private static string connectionStringTerms = @"Data Source=..\..\Database\Terms.db;Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists(@"..\..\Database\database.db"))
            {
                SQLiteConnection.CreateFile(@"..\..\Database\database.db");
                using (var connection = new SQLiteConnection(connectionStringDatabase))
                {
                    connection.Open();

                    //Creates Tables For Your Data (Absolutely didnt still half of this code from some youtube tutorial/StackOverflow)
                    string createPlayerTableQuery = @"
                            CREATE TABLE IF NOT EXISTS Player (
	                        Player_ID	INTEGER NOT NULL,
	                        Username	TEXT NOT NULL,
	                        Password	TEXT NOT NULL,
	                        PRIMARY KEY(Player_ID AUTOINCREMENT));";

                    /*
                        Game_ID	INTEGER NOT NULL,
	                    Gamemode	TEXT NOT NULL,
	                    Score	INTEGER NOT NULL,
	                    Username	TEXT NOT NULL,
	                    PRIMARY KEY(Game_ID AUTOINCREMENT),
	                    FOREIGN KEY(Username) REFERENCES Player(Username)*/

                    string createGameTableQuery = @"
                            CREATE TABLE IF NOT EXISTS Game (
	                        Game_ID	INTEGER NOT NULL,
	                        Gamemode	TEXT NOT NULL,
	                        Score	INTEGER NOT NULL,
	                        Username	TEXT NOT NULL,
	                        PRIMARY KEY(Game_ID AUTOINCREMENT));";

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = createPlayerTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createGameTableQuery;
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
        }



        public static string[] InitializeTerms(string termType)
        {
            List<string> list = new List<string>();

            using (var connection = new SQLiteConnection(connectionStringTerms))
            {
                connection.Open();
                string selectTerms;
                if (termType != "*")
                {
                    selectTerms = $"SELECT Word FROM Terms WHERE Category like \"{termType}\"";
                }
                else selectTerms = $"SELECT Word FROM Terms";


                SQLiteDataReader data = null;
                using (SQLiteCommand command = connection.CreateCommand())
                {

                    command.CommandText = selectTerms;
                    using (data = command.ExecuteReader())
                    {

                        if (data.HasRows)
                        {
                            while (data.Read())
                            {
                                list.Add(data.GetValue(0).ToString());
                            }
                        }
                    }
                }

                connection.Close();
            }
            return list.ToArray();
        }

        public static string[] InitializeRecords(string recordType)
        {
            List<string> list = new List<string>();

            using (var connection = new SQLiteConnection(connectionStringDatabase))
            {
                connection.Open();
                string selectTerms;
                if (recordType != "All Terms")
                {
                    selectTerms = $" SELECT Score, Username FROM Game WHERE Gamemode like '{recordType}' ORDER BY Score DESC limit 5";
                }
                else selectTerms = $" SELECT Score, Username FROM Game WHERE Gamemode like 'All' ORDER BY Score DESC limit 5";


                SQLiteDataReader data = null;
                using (SQLiteCommand command = connection.CreateCommand())
                {

                    command.CommandText = selectTerms;
                    using (data = command.ExecuteReader())
                    {

                        if (data.HasRows)
                        {
                            while (data.Read())
                            {
                                list.Add(data.GetValue(0).ToString());
                                list.Add(data.GetValue(1).ToString());
                            }
                        }
                    }
                }
                while (list.Count < 10)
                {
                    list.Add("-");
                    list.Add("-----");
                }
                connection.Close();
            }
            return list.ToArray();
        }

        public static void DeleteRecord(string gamemode)
        {
            using (var connection = new SQLiteConnection(connectionStringDatabase))
            {
                connection.Open();
                string registrationSQL = $"DELETE  FROM Game Where Gamemode like '{gamemode}'";


                using (var command = new SQLiteCommand(registrationSQL, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            return;
        }

        public static bool accountIsAlreadyMade(string Name)
        {
            List<string> list = new List<string>();

            using (var connection = new SQLiteConnection(connectionStringDatabase))
            {
                connection.Open();
                string selectTerms = $"SELECT Username FROM Player";


                SQLiteDataReader data = null;
                using (SQLiteCommand command = connection.CreateCommand())
                {

                    command.CommandText = selectTerms;
                    using (data = command.ExecuteReader())
                    {
                        if (data.HasRows)
                        {
                            while (data.Read())
                            {
                                list.Add(data.GetValue(0).ToString());
                            }
                        }
                    }
                }
                connection.Close();


                if (list.Contains(Name)) return true;
                return false;
            }
        }

        public static void RegistrationInDatabase(string username, string password)
        {
            using (var connection = new SQLiteConnection(connectionStringDatabase))
            {
                connection.Open();
                string registrationSQL = $"INSERT INTO Player (Username,Password) VALUES ('{username}','{password}')";


                using (var command = new SQLiteCommand(registrationSQL, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            return;
        }

        public static bool LoginValidator(string username, string password)
        {
            if (username == null || password == null) return false;

            if (accountIsAlreadyMade(username))
            {
                using (var connection = new SQLiteConnection(connectionStringDatabase))
                {
                    connection.Open();
                    string selectTerms = $"select Password FROM Player WHERE Username like '{username}';";

                    SQLiteDataReader data = null;
                    using (SQLiteCommand command = connection.CreateCommand())
                    {

                        command.CommandText = selectTerms;
                        using (data = command.ExecuteReader())
                        {

                            if (data.HasRows)
                            {
                                data.Read();
                                if (password.Equals(data.GetValue(0).ToString())) return true;
                                connection.Close();
                                return false;
                            }
                            else
                            {
                                connection.Close();
                                return false;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static void ScoreRegistration(string username, int score, string gamemode)
        {
            using (var connection = new SQLiteConnection(connectionStringDatabase))
            {
                connection.Open();
                string addRecord = $"INSERT INTO Game (Gamemode,Score,Username) VALUES ('{gamemode}',{score},'{username}')";

                using (var command = new SQLiteCommand(addRecord, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
