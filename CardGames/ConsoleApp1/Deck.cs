using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public Deck()
        {
            AssembleDeck();
        }
        private void AssembleDeck()
        {
            Cards = new List<Card>();
            var faces = Enum.GetValues(typeof(Face));
            var colors = Enum.GetValues(typeof(Color));
            //Add 4 Wild cards and 4 Draw Four Cards
            for (var i = 0; i < 4; i++)
            {
                Cards.Add(new Card(Face.WILD, Color.WILD));
                Cards.Add(new Card(Face.DRAW_FOUR, Color.WILD));
            }

            //Add all number cards with zeroes included
            foreach (Color color in colors)
            {
                foreach (Face face in faces)
                {
                    if (
                        face == Face.DRAW_FOUR || face == Face.WILD
                                               || color == Color.WILD)
                    {
                        continue;
                    }
                    var card = new Card(face, color);
                    Cards.Add(card);
                }
            }

            //Add all number cards without zeroes included
            //There are only 4 zeroes in a deck (1 for each color)
            foreach (Color color in colors)
            {
                foreach (Face face in faces)
                {
                    if (
                        face == Face.DRAW_FOUR || face == Face.WILD
                                               || color == Color.WILD || face == Face.ZERO)
                    {
                        continue;
                    }
                    var card = new Card(face, color);
                    Cards.Add(card);
                }
            }
        }
        public void ShuffleDeck()
        {
            Cards = GetShuffledDeck().ToList();
        }
        private IEnumerable<Card> GetShuffledDeck()
        {
            var cardArray = Cards.ToArray();
            var rng = new Random();
            for (var i = 0; i < cardArray.Length; i++)
            {
                int swapIndex = rng.Next(cardArray.Length - 1);
                yield return cardArray[swapIndex];
                cardArray[i] = cardArray[swapIndex];
            }
        }
        public List<Card> DrawCards(int numCards = 1)
        {
            var cards = new List<Card>();
            for (int i = 0; i < numCards; i++)
            {
                var card = Cards.First();
                Cards.Remove(card);
                cards.Add(card);
            }
            return cards;
        }
        public Hand DealHand()
        {
            var dealHand = new List<Card>();
            for (int i = 0; i < 7; i++)
            {
                var cardsToAdd = DrawCards();
                dealHand.AddRange(cardsToAdd);
            }
            var hand = new Hand(dealHand);
            return hand;
        }
    }
}
