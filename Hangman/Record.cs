using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    internal class Record
    {
        private string[] records;
        private string recordType = null;
        private Player player = null;
        private string gamemode = null;

        public Record(Player player, string[] records, string recordType, string gamemode)
        {
            this.records = records;
            this.recordType = recordType;
            this.player = player;
            this.gamemode = gamemode;
            writeTUI();
        }

        public void writeTUI()
        {
            Console.Clear();
            Console.WriteLine("Records For Category: " + recordType);
            Console.WriteLine(@"Score{0,5}Name", ' ');
            for (int i = 0; i < records.Length; i += 2)
            {
                Console.WriteLine(@"{0,-5}{1,5}{2}", records[i], ' ', records[i + 1]);
            }
            Console.WriteLine("\n1 Delete Records");
            Console.WriteLine("0 Go Back");

            string answerText = Console.ReadLine();
            int answer = 3;
            if (answerText.Length == 1 && char.IsDigit(answerText, 0)) answer = Convert.ToInt32(answerText);

            if (answer == 1)
            {
                DatabaseHelper.DeleteRecord(gamemode);
                new Record(player, DatabaseHelper.InitializeRecords(gamemode), recordType, gamemode);
            }
            else if (answer == 0)
            {
                new RecordFactory(player);
            }
            else writeTUI();

            return;
        }
    }
}
