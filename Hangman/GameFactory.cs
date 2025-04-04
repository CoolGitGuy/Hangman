using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    internal class GameFactory
    {
        private Player player;


        public GameFactory(Player player)
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
                getGame(answer);
            else writeTui();
        }
        public Game getGame(int gameType) //Chooses a gamemode based on gameType 
        {
            switch (gameType)
            {
                case 1:
                    string[] allTerms = DatabaseHelper.InitializeTerms("*");
                    return new Game(allTerms, "All", player);
                case 2:
                    string[] celebrities = DatabaseHelper.InitializeTerms("Celebrity");
                    return new Game(celebrities, "Celebrity", player);
                case 3:
                    string[] musicalInstruments = DatabaseHelper.InitializeTerms("Instrument");
                    return new Game(musicalInstruments, "Instrument", player);
                case 4:
                    string[] animals = DatabaseHelper.InitializeTerms("Animal");
                    return new Game(animals, "Animal", player);
                case 5:
                    string[] geography = DatabaseHelper.InitializeTerms("Geography");
                    return new Game(geography, "Geography", player);
                case 6:
                    string[] brands = DatabaseHelper.InitializeTerms("Brands");
                    return new Game(brands, "Brand", player);
                case 0:
                    new Menu(player);
                    return null;
                default:
                    new GameFactory(player);
                    return null;
            }
        }
    }
}
