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
            switch(MainMenu())
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    Login();
                    break;
                case 2:
                    Register();
                    break;
                default:
                    name = "Guest";
                    password = "Guest";
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

        public void Register()
        { 
            Console.Clear();

            Console.Write("Name:  ");
            name = Console.ReadLine();
            Console.Write("Password:  ");
            password = Console.ReadLine();
        }

        public void Login()
        {
            Console.Clear();

            Console.Write("Name: ");
            name = Console.ReadLine();
            Console.Write("Password: ");
            password = Console.ReadLine();
        }
    }
}
