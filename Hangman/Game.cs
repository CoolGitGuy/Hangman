using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    internal class Game
    {
        private int health = 5;
        string[] terms = { "Novak", "Boris", "Jovan" };
        public Game()
        {
            GameLogic();
        }

        public int Health { get => health; set => health = value; }

        public void GameLogic()
        {
            Console.Clear();
            string term = terms[new Random().Next(terms.Length)];

            string guess = Console.ReadLine();

            if(String.Equals(term,guess))
            {
                Console.WriteLine("Bravo Boss");
            }

        }
    }
}
