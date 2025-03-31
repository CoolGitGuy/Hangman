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
        private string[] terms = { "AAtrox" };
        private HashSet<char> guessedCharacters = new HashSet<char>();
        private string hiddenWord = null;

        public Game(){ //This one is used when you want to start a new game
            string term = terms[new Random().Next(terms.Length)];
            hiddenWord = new string('_', term.Length);
            GameLogic(term);
        }

        private Game(int score,int health) //Onto the next word
        {
            this.score = score;
            this.health = health;

            string term = terms[new Random().Next(terms.Length)];
            hiddenWord = new string('_', term.Length);
            GameLogic(term);
        }


        public void GameLogic(string term)
        {
            string guess = writeTUI();

            if (health == 0) // If health hits 0 player looses
            {
                typeWriterEffect("You Lose! :(\n");
                return;
            }
            if (String.Equals(term, guess)) // if the guessed word matches a term, go onto the next word
            {
                guessedWordPoints(term);
                Console.WriteLine("You Guessed it!");
                System.Threading.Thread.Sleep(600);
                new Game(score, ++health);
            }
            else if (guess.Length == 1) // If a player puts a character instead
            {
                if (guessedCharacters.Contains(guess[0]))
                {
                    GameLogic(term);
                    return;
                }    
                underscoreLogic(term, guess[0]);
                guessedCharacters.Add(guess[0]);

                if (!(hiddenWord == term)) GameLogic(term); // If the hiddenWord doesn't match the term then continue the game
                else
                {
                    Console.WriteLine("You Guessed it!");
                    System.Threading.Thread.Sleep(600);
                    new Game(score,++health);
                }
            }
            else // if a guess doesn't match with a word
            {
                health -= 1;
                GameLogic(term);
            }
        }

        public void underscoreLogic(string term,char guess) //Makes the Term Hidden and if the character is guessed, makes the character in the term visible
        {
            char[] hiddenWordCharacters = hiddenWord.ToCharArray();
            for(int i = 0; i< term.Length; i++)
            {
                if (term[i] == guess)
                {
                    hiddenWordCharacters[i] = guess;
                }
            }
            if(!hiddenWord.Equals(new string(hiddenWordCharacters)))score += 2;
            else health-= 1;
            hiddenWord = new string(hiddenWordCharacters);
        }


        public string writeTUI()
        {
            Console.Clear();
            Console.WriteLine("Health: " + health + " Score: " + score + "\n");
            Console.WriteLine("{0,3}_____{1,-1}{0,4}" + hiddenWord, ' ', (health <= 4) ? "," : "");
            Console.WriteLine("{0,3}| /  {1,-1} {0,4}", ' ',(health <= 4) ? ";" : "");
            Console.WriteLine("{0,3}|/   {1,-1}{0,4}{2}", ' ', (health <= 3 ) ? "O" : "", new string(guessedCharacters.ToArray()));
            Console.WriteLine("{0,3}|   {1,-1}{0,4}", ' ', (health <= 2 || health <= 1) ? ((health == 2) ? " |" : @"/|\") : "");
            Console.WriteLine("{0,3}|   {1,-1}{0,4}", ' ', (health <= 0) ? @"/ \" : "");
            Console.WriteLine("{0,3}|    {0,4}", ' ');
            Console.Write("{0,2}_|_     {0,3}", ' ' );
            if(health !=0)return Console.ReadLine();
            return null;
        }


        public void typeWriterEffect(string message)
        {
            for(int i = 0; i<message.Length; i++)
            {
                Console.Write(message[i]);
                System.Threading.Thread.Sleep(150);
            }
        }
        public void guessedWordPoints(string term)
        {
            for(int i =0; i<term.Length;i++)
            {
                if (hiddenWord[i] != term[i] && !guessedCharacters.Contains(term[i]))
                {
                    score += 3;
                    guessedCharacters.Add(term[i]);
                }
            }
        }
        public int Health { get => health; set => health = value; }
        public int Score { get => score; set => score = value; }

    }
}
