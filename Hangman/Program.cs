using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseHelper.InitializeDatabase();
            new Menu();
            Console.ReadLine();
        }
    }
}
