using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class DiscardPile
    {
        public List<Card> cards { get; set; }
        public Color CurrentColor { get; set; }
        public Card topCard => cards.Last();
        public DiscardPile()
        {
            cards = new List<Card>();
        }
        public void AddCard(Card card, Color chosenColor = Color.WILD)
        {
            if (card.Color == Color.WILD)
            {
                CurrentColor = chosenColor;
            }
            else
            {
                CurrentColor = card.Color;
            }
            cards.Add(card);
        }
        public List<Card> GetNewDeck()
        {
            var newDeck = cards.GetRange(0, cards.Count);
            cards.RemoveRange(0, cards.Count);
            return newDeck;
        }
    }
}
