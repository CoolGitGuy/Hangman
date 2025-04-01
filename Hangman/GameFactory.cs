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
        public GameFactory() {
            writeTui(true);
        }

        public GameFactory(Player player)
        {
            this.player = player;
            writeTui(false);
        }

        public void writeTui(bool isZeroOrBeginning)
        {
            if(isZeroOrBeginning)player = new Player();
            Console.Clear();
            Console.WriteLine("1   All Terms");
            Console.WriteLine("2   Celebrities");
            Console.WriteLine("3   Musical Instruments");
            Console.WriteLine("4   Animals");
            Console.WriteLine("5   Geography");
            Console.WriteLine("6   Animals");
            Console.WriteLine("\n0   Log Out");

            string answerText = Console.ReadLine();
            int answer = 10;
            if (answerText.Length == 1 && char.IsDigit(answerText,0)) answer = Convert.ToInt32(answerText);


            if (answer >= 0 && answer <= 9) 
                    getGame(answer);
            else writeTui(false);
        }
        public Game getGame(int gameType) 
        { 
            switch(gameType) { 
                case 1:
                    string[] allTerms = { "Novak Djokovic" ,"Pig","Potato"};
                    return new Game(allTerms,player);
                case 2:
                    string[] celebrities = { "Novak Djokovic", "Cristiano Ronaldo", "Lionel Messi" };
                    return new Game(celebrities, player);
                case 3:
                    string[] musicalInstruments = { "piano", "Cristiano Ronaldo", "Lionel Messi" };
                    return new Game(musicalInstruments, player);
                case 4:
                    string[] animals = { "Cat", "Dog", "Pig" };
                    return new Game(animals, player);
                case 5:
                    string[] geography = { "Belgrade", "Toronto", "Bosnia and Hertzegovina" };
                    return new Game(geography, player);
                case 6:
                    string[] brands = { "Nike", "Adidas", "Doritos" };
                    return new Game(brands, player);
                case 0:
                    writeTui(true);
                    return null;
                default: return null;
            }
        }
    }
}
