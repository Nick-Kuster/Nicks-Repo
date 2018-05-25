using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class DeckTests
    {
        [TestMethod]
        public void TestDeckBuild()
        {
            Deck deck = new Deck();
            PrintCardList(deck.Cards);
            Assert.IsTrue(deck.Cards.Count == 108);
        }

        [TestMethod]
        public void TestDeckShuffle()
        {
            Deck deck = new Deck();
            deck.ShuffleDeck();
            PrintCardList(deck.Cards);
            Assert.IsTrue(deck.Cards.Count == 108);
        }

        [TestMethod]
        public void TestDeckDraw()
        {
            Deck deck = new Deck();
            deck.ShuffleDeck();
            var hand = deck.DealHand();
            PrintCardList(hand.Cards);
            Debug.WriteLine("Deck Size: " + deck.Cards.Count);
        }

        private void PrintCardList(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                Debug.WriteLine(card.Face.ToString() + " | " + card.Color.ToString());
            }
        }
    }
}
