using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    internal class Player
    {
        private string name;
        private string password;

        public Player()
        {
            switch (MainMenu())
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    Authentification(1);
                    break;
                case 2:
                    Authentification(2);
                    break;
                default:
                    new Player();
                    break;
            }
        }

        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }

        public int MainMenu()
        {
            Console.Clear();
            Console.WriteLine("1   Login");
            Console.WriteLine("2   Register\n");
            Console.WriteLine("0   Exit");

            try { return int.Parse(Console.ReadLine()); }
            catch { return MainMenu(); }
        }

        public void Authentification(int type) //Registration (type 2) and Login (type 1)
        {
            Console.Clear();

            Console.Write("Name:  ");
            name = Console.ReadLine();
            Console.Write("Password:  ");
            password = Console.ReadLine();

            if (type == 1) LoginChecker();
            if (type == 2) RegisterChecker();
        }

        public void LoginChecker() //Checks if Login was valid
        {
            string answer = null;
            Console.ForegroundColor = ConsoleColor.Red;
            if (!DatabaseHelper.LoginValidator(name, password))
            {
                Console.WriteLine("\nUsername/Password Is Not Valid");
                Console.ResetColor();
                Console.WriteLine("\nType Any Key To Go Back...\n1 To Try Again");
                answer = Console.ReadLine();
                Console.ResetColor();
                if (answer != "1") new Player();
                else Authentification(1);
            }
            Console.ResetColor();
        }

        public void RegisterChecker() //Checks if Registration was valid
        {
            string answer = null;
            Console.ForegroundColor = ConsoleColor.Red;
            if (DatabaseHelper.accountIsAlreadyMade(name))
            {
                Console.WriteLine("\nAccount With That Username Is Already Taken!");
                Console.ResetColor();
                Console.WriteLine("\nType Any Key To Go Back...\n1 To Try Again");
                answer = Console.ReadLine();

                if (answer != "1") new Player();
                else Authentification(2);
                return;
            }
            else if (name == "" || password == "")
            {
                Console.WriteLine("\nYou Forgot To Type In Your Name/Password!");
                Console.ResetColor();
                Console.WriteLine("\nType Any Key To Go Back...\n1 To Try Again");
                answer = Console.ReadLine();

                if (answer=="" || answer[0] == '1') new Player();
                else Authentification(2);
                return;
            }
            DatabaseHelper.RegistrationInDatabase(name, password);
            Console.ResetColor();
        }
    }
}
