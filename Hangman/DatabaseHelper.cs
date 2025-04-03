using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Hangman
{
    internal class DatabaseHelper
    {
        private static string connectionStringDatabase = @"Data Source=..\..\Database\database.db;Version=3;";
        public static void InitializeDatabase()
        {
            if(!File.Exists(@"..\..\Database\database.db"))
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

                    string createGameTableQuery = @"
                            CREATE TABLE IF NOT EXISTS Game (
	                        Game_ID	INTEGER NOT NULL,
	                        Gamemode	TEXT NOT NULL,
	                        Score	INTEGER NOT NULL,
	                        Player_ID	INTEGER NOT NULL,
	                        PRIMARY KEY(Game_ID AUTOINCREMENT),
	                        FOREIGN KEY(Player_ID) REFERENCES Player(Player_ID));";

                    using(var command = new SQLiteCommand(connection)) 
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

        private static string connectionStringTerms = @"Data Source=..\..\Database\Terms.db;Version=3;";

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
                    data = command.ExecuteReader();

                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            list.Add(data.GetValue(0).ToString());
                        }
                    }
                }
                
                connection.Close();
            }
                return list.ToArray();
        }
    }
}

/*using (SQLiteDataReader MyDataReader = command.ExecuteReader())
                    {
                        while(MyDataReader.Read())
                        {
                            string word = MyDataReader["Word"].ToString();
                            if(String.IsNullOrEmpty(word))terms[i] = word;
                        }
                        
                    }*/