using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hangman
{
    internal class Game
    {
        private int health = 5;
        private int score = 0;
        private string[] terms = { "Novak", "Boris", "Jovan" };
        private string hiddenWord = null;

        public Game(){
            string term = terms[new Random().Next(terms.Length)];
            hiddenWord = new string('_', term.Length);
            GameLogic(term);
        }


        public void GameLogic(string term)
        {
            if(health == 0){
                Console.WriteLine("You Lose!");
                return;
            }

            Console.Clear();
            Console.WriteLine("Health: " + health);
            Console.WriteLine(term);
            Console.WriteLine(hiddenWord);

            string guess = Console.ReadLine();


            if (String.Equals(term, guess))
            {
                termGuessed();
            }
            else if (guess.Length == 1)
            {
                underscoreLogic(term, guess[0]);
                GameLogic(term);
            }
            else
            {
                health -= 1;
                GameLogic(term);
            }
        }

        public void termGuessed()
        {
            Console.Clear();
            Console.WriteLine(
                "You Guessed It!\n"+
                "Press Any Key"+
                "");

        }

        public void underscoreLogic(string term,char guess)
        {
            char[] hiddenWordCharacters = hiddenWord.ToCharArray();
            for(int i = 0; i< term.Length; i++)
            {
                if (term[i] == guess)
                {
                    hiddenWordCharacters[i] = guess;
                }
            }
            hiddenWord = new string(hiddenWordCharacters);
        }

        public int Health { get => health; set => health = value; }
        public int Score { get => score; set => score = value; }

    }
}
