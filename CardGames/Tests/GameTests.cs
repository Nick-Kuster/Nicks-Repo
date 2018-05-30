using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConsoleApp1;
using ConsoleApp1.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class GameTests
    {
        private GameLogic _logic;
        [TestMethod]
        public void GameInitTest()
        {
            var pList = CreatePlayerList(2);
            Deck deck = new Deck();
            Game game = InitGame(pList, deck);
            Debug.WriteLine("Stop");
        }
        [TestMethod]
        public void Test500PointWin()
        {
            _logic = new GameLogic();
            Game game = GetBasicSetupGame(5);
            bool haveWinner = _logic.CheckForWinner(game);
            Assert.IsTrue(!haveWinner);
            game.Players[0].Score = 499;
            haveWinner = _logic.CheckForWinner(game);
            Assert.IsTrue(!haveWinner);
            game.Players[0].Score++;
            haveWinner = _logic.CheckForWinner(game);
            Assert.IsTrue(haveWinner);
        }

        private Game GetBasicSetupGame(int numPlayers)
        {
            var pList = CreatePlayerList(numPlayers);
            Deck deck = new Deck();
            return InitGame(pList, deck);
        }
        private List<Player> CreatePlayerList(int numPlayers)
        {
            var p = new List<Player>();
            for (int i = 0; i < numPlayers; i++)
            {
                p.Add(CreatePlayer($"Player {i.ToString()}"));
            }
            return p;
        }
        private Player CreatePlayer(string name, int score = 0)
        {
            return new Player()
            {
                PlayerName = name,
                Score = score
            };
        }
        private Game InitGame(List<Player> p, Deck deck)
        {
            return new Game(p, deck, new DiscardPile());
        }

    }
}
