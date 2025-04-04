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
        private string[] terms;
        private string gamemode = null;
        private HashSet<char> guessedCharacters = new HashSet<char>();
        private string hiddenWord = null;
        private int pointCounter = 0;
        private int round = 1;
        private Player player;


        public Game(string[] terms, string gamemode, Player player) //This one is used when you want to start a new game
        {
            this.terms = terms;
            this.player = player;
            this.gamemode = gamemode;

            string term = terms[new Random().Next(terms.Length)];
            hiddenWord = new string('_', term.Length);
            checkSpace(term);
            GameLogic(term);
        }

        private Game(int score, int health, int pointCounter, int round, string[] terms, string gamemode, Player player) //Onto the next word
        {
            this.score = score;
            this.health = health;
            this.pointCounter = pointCounter;
            this.round = round;
            this.terms = terms;
            this.player = player;
            this.gamemode = gamemode;

            string term = terms[new Random().Next(terms.Length)];
            hiddenWord = new string('_', term.Length);
            checkSpace(term);
            GameLogic(term);
        }

        public void GameLogic(string term)
        {
            string guess = writeTUI();
            pointCounterReset();

            if (health == 0) // If health hits 0 player looses
            {
                Console.ForegroundColor = ConsoleColor.Red;
                typeWriterEffect("You Lose! :(\n");
                Console.ResetColor();
                DatabaseHelper.ScoreRegistration(player.Name, score, gamemode);
                new GameFactory(player);
                return;
            }
            if (String.Equals(term.ToLower(), guess.ToLower())) // if the guessed word matches a term, go onto the next word
            {
                guessedWordPoints(term);
                Console.WriteLine("You Guessed it!");
                System.Threading.Thread.Sleep(600);
                pointCounterReset();
                new Game(score, ++health, pointCounter, ++round, terms, gamemode, player);
            }
            else if (guess.Length == 1) // If a player puts a character instead
            {
                if (guess == " ")
                {
                    GameLogic(term);
                    return;
                }
                if (guessedCharacters.Contains(guess.ToLower()[0]))
                {
                    GameLogic(term);
                    return;
                }
                underscoreLogic(term, guess[0]);
                guessedCharacters.Add(guess.ToLower()[0]);

                if (!(hiddenWord == term)) GameLogic(term); // If the hiddenWord doesn't match the term then continue the game
                else
                {
                    Console.Write("You Guessed it!");
                    System.Threading.Thread.Sleep(600);
                    new Game(score, ++health, pointCounter, ++round, terms, gamemode, player);
                }
            }
            else // if a guess doesn't match with a word
            {
                if (!String.IsNullOrWhiteSpace(guess)) health -= 1;
                GameLogic(term);
            }
        }

        public void underscoreLogic(string term, char guess) //Makes the Term Hidden and if the character is guessed, makes the character in the term visible
        {
            char[] hiddenWordCharacters = hiddenWord.ToCharArray();
            string lowerTerm = term.ToLower();
            for (int i = 0; i < term.Length; i++)
            {
                if (lowerTerm[i] == guess.ToString().ToLower()[0])
                {
                    hiddenWordCharacters[i] = term[i];
                }
            }
            if (!hiddenWord.Equals(new string(hiddenWordCharacters))) { score += 2; pointCounter += 2; }
            else health -= 1;
            hiddenWord = new string(hiddenWordCharacters);
        }


        public string writeTUI() //Text User Interface
        {
            Console.Clear();
            //Console.WriteLine("Name: " + player.Name);
            Console.WriteLine("Health: " + health + " Score: " + score + "\nRound: " + round);
            Console.WriteLine("{0,3}_____{1,-1}{0,4}" + hiddenWord, ' ', (health <= 4) ? "," : "");
            Console.WriteLine("{0,3}| /  {1,-1} {0,4}", ' ', (health <= 4) ? ";" : "");
            Console.WriteLine("{0,3}|/   {1,-1}{0,4}{2}", ' ', (health <= 3) ? "O" : "", new string(guessedCharacters.ToArray()));
            Console.WriteLine("{0,3}|   {1,-1}{0,4}", ' ', (health <= 2 || health <= 1) ? ((health == 2) ? " |" : @"/|\") : "");
            Console.WriteLine("{0,3}|   {1,-1}{0,4}", ' ', (health <= 0) ? @"/ \" : "");
            Console.WriteLine("{0,3}|    {0,4}", ' ');
            Console.Write("{0,2}_|_     {0,3}", ' ');
            if (health != 0) return Console.ReadLine();
            return null;
        }

        public void typeWriterEffect(string message) //Adds typewriter effect to a message
        {
            for (int i = 0; i < message.Length; i++)
            {
                if (i % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write(message[i]);
                Console.ResetColor();
                System.Threading.Thread.Sleep(150);
            }
        }

        public void checkSpace(string term)
        {
            if (term.Contains(' '))
            {
                StringBuilder stringBuilder = new StringBuilder(hiddenWord);
                stringBuilder[term.IndexOf(' ')] = ' ';
                hiddenWord = stringBuilder.ToString();
            }
        }

        public void guessedWordPoints(string term) //3 points for each unique hidden character (no duplicates) after player quesses a full word right
        {
            for (int i = 0; i < term.Length; i++)
            {
                if (hiddenWord.ToLower()[i] != term.ToLower()[i] && !guessedCharacters.Contains(term.ToLower()[i]))
                {
                    score += 3;
                    pointCounter += 3;
                    guessedCharacters.Add(term[i]);
                }
            }
        }

        public void pointCounterReset()
        {
            if (pointCounter >= 50) //Adds 1 health for every 50 score points 
            {
                pointCounter -= 50;
                ++health;
            }
        }

        public int Health { get => health; set => health = value; }
        public int Score { get => score; set => score = value; }

    }
}
