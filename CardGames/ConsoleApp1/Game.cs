using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Game
    {
        public int Turn { get; set; }
        public TurnDirection Direction { get; set; }
        public bool GameOver { get; set; }
        public bool ReturnToDealer { get; set; }
        public bool StayOnPlayer { get; set; }
        public bool SkipNextPlayer { get; set; }
        public List<Player> Players { get; set; }
        public Player Winner { get; set; }
        public int WinnerScore { get; set; }
        public Deck Deck { get; set; }
        public DiscardPile DiscardPile { get; set; }
        public Game(List<Player> players, Deck deck, DiscardPile discardPile)
        {
            Players = players;
            Deck = deck;
            deck.ShuffleDeck();
            DealCards(deck);
            Direction = TurnDirection.Clockwise;
            Turn = 0;
            DiscardPile = discardPile;
        }
        private void DealCards(Deck deck)
        {
            foreach (Player p in Players)
            {
                p.Hand = deck.DealHand();
            }
        }

        public List<Card> DrawCardsFromDeck(int numCards = 1)
        {
            //If there aren't enough cards in deck to be shuffled,
            //This will shuffle the discard deck back into main deck.
            if (Deck.Cards.Count - numCards <= 0)
            {
                ResetDeck();
                Deck.ShuffleDeck();
            }
            return Deck.DrawCards(numCards);
        }

        public void ResetDeck()
        {
            //persist top card of discard pile
            var topCard = DiscardPile.topCard;
            //remove the top card from the discard pile
            DiscardPile.cards.Remove(topCard);
            //replace the empty deck with the cards from the discard pile
            Deck.Cards.AddRange(DiscardPile.cards);
            //Clear out the discard pile
            DiscardPile.cards.Clear();
            //Add the top card back to the discard pile, since it needs to 
            //be the top card for the next play.
            DiscardPile.cards.Add(topCard);
        }
        public Player GetNextPlayer()
        {
            switch (Direction)
            {
                //If going clockwise, check to see if we need to
                //Return to player 1
                case TurnDirection.Clockwise:    
                    if (Turn >= Players.Count - 1)
                    {
                        return Players[0];
                    }
                    return Players[Turn + 1];
                //If going counterclockwise, check to see if we need
                //to return to last player in list.
                case TurnDirection.CounterClockwise:
                    if (Turn <= 0)
                    {
                        return Players.Last();
                    }
                    return Players[Turn - 1];
                default:
                    throw new ArgumentException("Whoops!");
            }
        }
        public void EndTurn()
        {
            if (ReturnToDealer)
            {
                Turn = 0;
                ReturnToDealer = false;
                return;
            }
            if (SkipNextPlayer)
            {
                RotateTurn();
                RotateTurn();
                SkipNextPlayer = false;
                return;
            }
            if (StayOnPlayer)
            {
                //Do nothing if we are staying on the same player.
                StayOnPlayer = false;
                return;
            }
            if (Players[Turn].Hand.Cards.Count == 0)
            {
                GameOver = true;
                Winner = Players[Turn];
            }
            //If none of the above conditions are met, we can just rotate
            //one turn in whatever direction we are going.
            RotateTurn();
        }
        public void EndGame()
        {

        }

        private void RotateTurn()
        {
            switch (Direction)
            {
                //If going clockwise, check to see if we need to
                //Return to player 1
                case TurnDirection.Clockwise:
                    Turn++;
                    if (Turn >= Players.Count)
                    {
                        Turn = 0;
                    }
                    return;
                //If going counterclockwise, check to see if we need
                //to return to last player in list.
                case TurnDirection.CounterClockwise:
                    Turn--;
                    if (Turn < 0)
                    {
                        Turn = Players.Count - 1;
                    }
                    return;
                default:
                    throw new ArgumentException("Whoops!");
            }
        }
        public enum TurnDirection
        {
            Clockwise,
            CounterClockwise
        }
    }
}
