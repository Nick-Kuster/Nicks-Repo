using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void GameInitTest()
        {
            var pList = new List<Player>();
            Player p1 = new Player("Nick");
            Player p2 = new Player("Jef");
            pList.Add(p1);
            pList.Add(p2);
            Deck deck = new Deck();
            Game game = new Game(pList, deck, new DiscardPile());
            Debug.WriteLine("Stop");
        }
    }
}
