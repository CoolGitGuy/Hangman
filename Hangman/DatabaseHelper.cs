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
        private static string connectionString = @"Data Source=..\..\Database\database.db;Version=3;";
        public static void InitializeDatabase()
        {
            if(!File.Exists(@"..\..\Database\database.db"))
            {
                SQLiteConnection.CreateFile(@"..\..\Database\database.db");
                using (var connection = new SQLiteConnection(connectionString))
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
                }
            }
        }

    }
}
