using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Logic
{
    public class GameLogic
    {
        public Player GetNewPlayer(string name)
        {
            Player p = new Player(name);
            return p;
        }
        public Game GetNewGame(List<Player> players)
        {
            Deck deck = new Deck();
            DiscardPile discardPile = new DiscardPile();
            Game game = new Game(players, deck, discardPile);
            return game;
        }
        public void DrawFirstCard(Game game)
        {
            game.DiscardPile.AddCard(game.Deck.DrawCards()[0]);
        }

        public void EvaluateFirstCard(Game game, Card card)
        {
            //This applies the first card to the first player
            EvaluateCardRules(game, card, game.Players[0]);
        }
        public void EvaluateCardRules(Game game, Card card, Player nextPlayer)
        {
            switch (card.Face)
            {
                case Face.DRAW_FOUR:
                    DrawFour(game, nextPlayer);
                    return;
                case Face.DRAW_TWO:
                    DrawTwo(game, nextPlayer);
                    return;
                case Face.REVERSE:
                    Reverse(game);
                    return;
                case Face.SKIP:
                    Skip(game);
                    return;
                default:
                    return;
            }
        }
        public void Reverse(Game game)
        {
            switch (game.Direction)
            {
                case Game.TurnDirection.Clockwise:
                    game.Direction = Game.TurnDirection.CounterClockwise;
                    break;
                case Game.TurnDirection.CounterClockwise:
                    game.Direction = Game.TurnDirection.Clockwise;
                    break;
                default:
                    throw new ArgumentException("Game direction not set");
            }
        }
        public void Skip(Game game)
        {
            game.SkipNextPlayer = true;
            ////e.g. if we are on there are 3 players and Player 3 plays a skip,
            ////the turn needs to be set to 0 so that changing turns moves the turn
            ////to player 2 instead of making the turn "4", which the next turn method
            ////will revert back to player 1.
            //if (game.Turn + 1 > game.Players.Count)
            //{
            //    game.Turn = 0;
            //    return;
            //}
            //game.Turn++;
        }
        public void DrawFour(Game game, Player nextPlayer)
        {
            nextPlayer.Hand.AddCards(game.DrawCardsFromDeck(4));
            game.SkipNextPlayer = true;
        }
        public void DrawTwo(Game game, Player nextPlayer)
        {
            nextPlayer.Hand.AddCards(game.DrawCardsFromDeck(2));
            game.SkipNextPlayer = true;
        }
    }
}
