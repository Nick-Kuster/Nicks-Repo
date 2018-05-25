using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Logic;
using ConsoleApp1.View;

namespace ConsoleApp1.Controller
{
    public class GameController
    {
        private GameIO _io = new GameIO();
        private GameLogic _logic;
        private bool _playAgain;
        public GameController(GameIO io, GameLogic logic)
        {
            _io = io;
            _logic = logic;
            _playAgain = true;
        }
        public void StartGame()
        {
            var game = SetupGame();
            while (_playAgain)
            {
                BeginPlay(game, game.Players);
            }
        }
        private Game SetupGame()
        {
            int numPlayers = _io.WelcomeScreen();
            List<Player> players = new List<Player>();
            for (int i = 0; i < numPlayers; i++)
            {
                var name = _io.GetPlayerName(i);
                Player p = _logic.GetNewPlayer(name);
                players.Add(p);
            }
            _io.PrintPlayerList(players);
            return _logic.GetNewGame(players);
        }
        private void BeginPlay(Game game, List<Player> players)
        {
            _logic.DrawFirstCard(game);
            _logic.EvaluateFirstCard(game, game.DiscardPile.topCard);
            //If First Card drawn is Wildcard, the first player
            //chooses what color it will be.
            if (game.DiscardPile.topCard.Color == Color.WILD)
            {
                var p = players[game.Turn];
                _io.PrintTopDiscardCard(game.DiscardPile.topCard);
                _io.PrintPlayerHand(p.Hand);
                Color color = (Color)_io.ChooseWildCardColor();
                game.DiscardPile.CurrentColor = color;
            }
            while (!game.GameOver)
            {
                var p = players[game.Turn];
                _io.PrintPlayerTurn(players[game.Turn], game);
                _io.PrintTopDiscardCard(game.DiscardPile.topCard);
                _io.PrintPlayerHand(p.Hand);
                TakeTurn(p, game);
                if (p == null) continue; // If player decks out in TakeTurn phase, player is removed from game.
                UnoCheck(p, game);
                game.EndTurn();
                if (p.Hand.Cards.Count == 0)
                {
                    game.GameOver = true;
                }
            }
            _logic.EvaluateScore(game);
            bool playAgain = _io.GameOver(game) == 1;
            _playAgain = playAgain;
        }
        private void TakeTurn(Player p, Game game)
        {
            var validCard = false;
            while (!validCard)
            {
                var cardChoiceInt = _io.GetPlayerCardChoice(p.Hand);
                //If user needs to draw a card for not having playable card
                //They choose '0' which is -1 after subtracting 1 accounting for index
                if (cardChoiceInt == 0)
                {
                    if (game.Deck.Cards.Count > 0)
                    {
                        //Draw a card then set the turn so that it remains the players turn
                        //until they draw a playable card.
                        p.Hand.AddCards(game.DrawCardsFromDeck());
                        game.StayOnPlayer = true;
                        //Though Not a valid card, this resets the turn 
                        validCard = true;
                    }
                    else
                    {
                        _io.DeckOutMessage(p);
                        game.Deck.Cards.AddRange(p.Hand.Cards);
                        game.Players.Remove(p);
                    }
                }
                else
                {
                    var cardChoice = p.Hand.Cards[cardChoiceInt - 1];
                    if (cardChoice.Color == game.DiscardPile.CurrentColor
                        || cardChoice.Face == game.DiscardPile.topCard.Face
                        || cardChoice.Color == Color.WILD)
                    {
                        validCard = true;
                        game.DiscardPile.AddCard(cardChoice);
                        p.Hand.Cards.Remove(cardChoice);
                        if (cardChoice.Color == Color.WILD)
                        {
                            Color color = (Color)_io.ChooseWildCardColor();
                            game.DiscardPile.CurrentColor = color;
                        }
                        _logic.EvaluateCardRules(game, cardChoice, game.GetNextPlayer());
                    }
                    else
                    {
                        _io.InvalidCardMessage();
                    }
                }
            }
        }
        private void UnoCheck(Player p, Game g)
        {
            if (p.Hand.Uno)
            {
                var unoCaller = _io.UnoMessage();
                switch (unoCaller)
                {
                    case GameIO.UnoCaller.SELF:
                        _io.UnoNoPenalty();
                        break;
                    case GameIO.UnoCaller.OTHER_PLAYER:
                        _io.UnoPenaltyForSelf();
                        p.Hand.AddCards(g.DrawCardsFromDeck(2));
                        break;
                    case GameIO.UnoCaller.OTHER_PLAYER_INCORRECT:
                        var incorrectPlayer = _io.UnoPenaltyForOther(g.Players);
                        incorrectPlayer.Hand.AddCards(g.DrawCardsFromDeck(2));
                        break;
                    case GameIO.UnoCaller.NOBODY:
                        _io.UnoNoPenalty();
                        break;
                }
            }
        }
    }
}

