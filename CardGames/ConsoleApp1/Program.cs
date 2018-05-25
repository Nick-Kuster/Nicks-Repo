using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Controller;
using ConsoleApp1.Logic;
using ConsoleApp1.View;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            GameIO view = new GameIO();
            GameLogic logic = new GameLogic();
            GameController con = new GameController(view, logic);
            con.StartGame();
        }
    }
}
