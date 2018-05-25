using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Player
    {
        public Hand Hand;
        public string PlayerName;
        public int Score;
        public Player(string playerName = "Opy")
        {
            PlayerName = playerName;
        }
    }
}
