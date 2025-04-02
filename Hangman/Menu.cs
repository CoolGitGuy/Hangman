using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    internal class Menu
    {
        private Player player;
        public Menu() {
            writeMenuTUI(false);
        }

        public Menu(Player player)
        {
            this.player = player;
            writeMenuTUI(true);
        }

        public void writeMenuTUI(bool playerAlreadyDeclared) //Writes Text User Interface, (playerAlreadyDeclared = false) Make a new player/Log Out 
        {
            if(!playerAlreadyDeclared)player = new Player();
            
            Console.Clear();
            //Console.WriteLine("Name: " + player.Name);
            Console.WriteLine("1   New Game");
            Console.WriteLine("2   Records\n");
            Console.WriteLine("0 Log Out");

            string answerText = Console.ReadLine();
            int answer = 3;
            if (answerText.Length == 1 && char.IsDigit(answerText, 0)) answer = Convert.ToInt32(answerText);

            optionPicker(answer);
        }

        public void optionPicker(int answer)
        {
            switch(answer)
            {
                case 1:
                    new GameFactory(player);
                    break;
                case 2: //Konektovao bi sa GameFactory samo sto bi izmenio neke stvari
                    return;
                case 0:
                    writeMenuTUI(false);
                    break;
                default: 
                    writeMenuTUI(true);
                    break;
            }
        }
    }
}
