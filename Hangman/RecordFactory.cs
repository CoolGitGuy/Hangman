using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    internal class RecordFactory
    {
        private Player player;
        public RecordFactory(Player player)
        {
            this.player = player;
            writeTui();
        }

        public void writeTui() //Writes Text User Interface for Game Choosing Menu, (isZeroOrBeginning = true) Log Out 
        {

            Console.Clear();

            //Console.WriteLine("Name: " + player.Name);
            Console.WriteLine("1   All Terms");
            Console.WriteLine("2   Celebrities");
            Console.WriteLine("3   Musical Instruments");
            Console.WriteLine("4   Animals");
            Console.WriteLine("5   Geography");
            Console.WriteLine("6   Brands");
            Console.WriteLine("\n0   Go Back");

            string answerText = Console.ReadLine();
            int answer = 10;
            if (answerText.Length == 1 && char.IsDigit(answerText, 0)) answer = Convert.ToInt32(answerText);


            if (answer >= 0 && answer <= 9)
                getRecord(answer);
            else writeTui();
        }

        public void getRecord(int recordType)
        {
            string[] list;
            switch (recordType)
            {
                case 1:
                    list = DatabaseHelper.InitializeRecords("All Terms");
                    new Record(player,list, "All Terms", "All");
                    break;
                case 2:
                    list = DatabaseHelper.InitializeRecords("Celebrity");
                    new Record(player, list, "Celebrities", "Celebrity");
                    break;
                case 3:
                    list = DatabaseHelper.InitializeRecords("Instrument");
                    new Record(player, list, "Musical Instruments", "Instrument");
                    break;
                case 4:
                    list = DatabaseHelper.InitializeRecords("Animal");
                    new Record(player, list, "Animals", "Animal");
                    break;
                case 5:
                    list = DatabaseHelper.InitializeRecords("Geography");
                    new Record(player, list, "Geography", "Geography");
                    break;
                case 6:
                    list = DatabaseHelper.InitializeRecords("Brand");
                    new Record(player, list, "Brands", "Brand");
                    break;
                case 0:
                    new Menu(player);
                    break;
                default:
                    new RecordFactory(player);
                    break;
            }
        }
    }
}
