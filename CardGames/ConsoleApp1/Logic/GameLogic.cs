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

        public void EvaluateScore(Game game)
        {
            var winnerScore = 0;
            foreach (Player p in game.Players)
            {
                winnerScore += GetPlayerScore(p);
            }
            game.Winner.Score += winnerScore;
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

        private int GetPlayerScore(Player p)
        {
            var sum = 0;
            foreach (Card c in p.Hand.Cards)
            {
                sum += GetCardScore(c);
            }
            return sum;
        }

        private int GetCardScore(Card c)
        {
            switch (c.Face)
            {
                case Face.ZERO:
                    return 0;
                case Face.ONE:
                    return 1;
                case Face.TWO:
                    return 2;
                case Face.THREE:
                    return 3;
                case Face.FOUR:
                    return 4;
                case Face.FIVE:
                    return 5;
                case Face.SIX:
                    return 6;
                case Face.SEVEN:
                    return 7;
                case Face.EIGHT:
                    return 8;
                case Face.NINE:
                    return 9;
                case Face.DRAW_TWO:
                case Face.SKIP:
                case Face.REVERSE:
                    return 20;
                case Face.WILD:
                    return 50;
                default:
                    return 0;
            }
        }
    }
}
