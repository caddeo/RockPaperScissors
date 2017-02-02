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

            Console.WriteLine("Game done");
            Console.ReadKey();
        }

        Player[] _players;

        public void Run()
        {
            _players = null;

            var playing = true;
            while(playing)
            {
                var menu = GetMenu();
                for (int i = 0; i < menu.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {menu[i]}");
                }

                var selected = GetMenuSelect();

                if (0 >= selected)
                {
                    Console.WriteLine("Error: Input mistaken, please redo\n");
                    continue;
                }

                if (selected == 1)
                {
                    _players = CreatePlayers();
                    continue;
                }

                if(selected == 2)
                {
                    PlayGame();
                    continue;
                }

                if(selected == 3)
                {
                    playing = false;
                }
            }
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

        Player[] CreatePlayers()
        {
            Player[] players = new Player[2];

            for(int i=0; i<2; i++)
            {
                Console.WriteLine("Choose a name");
                var name = Console.ReadLine();

                players[i] = new Player(name);
            }

            return players;
        }

        void PlayGame()
        {
            var playing = true;
            
            while(playing)
            {
                if (_players == null)
                {
                    Console.WriteLine("Error: Please select names first");
                    return;
                }

                foreach (Player player in _players)
                {
                    PlayerChooseHand(player);
                }

                var winner = CompareHands(_players[0], _players[1]);

                if (winner == null)
                {
                    Console.WriteLine("Draw! Try again");
                    continue;
                }

                Console.Write($"{winner.Name} is the ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("winner");
                Console.ResetColor();
                playing = false;
            }
        }

        Player CompareHands(Player player1, Player player2) 
        {
            if(player1.Chosen == player2.Chosen)
            {
                return null;
            }

            if(player1.Chosen > player2.Chosen && player1.Chosen != Player.Hand.Scissor)
            {
                return player1;
            }

            return player2;
        }

        void PlayerChooseHand(Player player)
        {
            var playerChosen = false;
            while(!playerChosen)
            {
                
                Console.WriteLine($"{player.Name} choose a hand: \n1. Rock \n2. Paper \n3. Scissor \n");
                var input = Console.ReadKey(true);

                var selected = 0;
                var validated = int.TryParse(input.KeyChar.ToString(), out selected);
                var condition = selected >= 1 && selected <= 3;

                // Hvis det valgte nummer er mindre end 0 og det valgte nummer er større end 3
                if (!validated || (0 > selected || selected > 3))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("ERROR");
                    Console.ResetColor();
                    Console.Write(": Please enter a valid number\n");
                    continue;
                }

                player.Chosen = (Player.Hand) selected;
                playerChosen = true;
            }
        }


        public class Player
        {
            public string Name { get; set; }
            public Hand Chosen { get; set; }

            public Player(string name)
            {
                this.Name = name;
                this.Chosen = Hand.Empty;
            }

            public enum Hand
            {
                Empty,
                Rock,
                Paper,
                Scissor
            }
        }
    }
}
