using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        public static void Main(string[] args)
        {
            new Program().Run();

            Console.ReadKey();
        }


        public void Run()
        {
            var menu = GetMenu();
            for(int i=0; i<menu.Length; i++)
            {
                Console.WriteLine($"{i+1}. {menu[i]}");
            }

            var selected = GetMenuSelect();

            if(selected > -1)
            {
                Console.WriteLine("Hihihi");
            }

            Console.WriteLine("!");
        }

        string[] GetMenu()
        {
            string[] menu = new string[]
            {
                "Choose player names",
                "Start game",
                "Exit game"
        };

            return menu;
        }

        int GetMenuSelect()
        {
            var input = Console.ReadLine();

            var errorcode = -1;
        
            var validated = 0;
            if(!int.TryParse(input, out validated))
            {
                return errorcode;
            }

            if (validated < 0 && validated > GetMenu().Length)
            {
                return errorcode;
            }

            return validated;
        }


        public class Player
        {
            public string Name { get; protected set; }
            public Hand Chosen { get; protected set; }

            public Player(string name, Hand chosen)
            {
                this.Name = name;
                this.Chosen = chosen;
            }

            public enum Hand
            {
                Rock,
                Paper,
                Scissor
            }
        }
    }
}
