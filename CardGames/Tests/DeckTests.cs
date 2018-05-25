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
            AssertNumberOfCardsByColor(deck.Cards, Color.RED);
            AssertNumberOfCardsByColor(deck.Cards, Color.BLUE);
            AssertNumberOfCardsByColor(deck.Cards, Color.GREEN);
            AssertNumberOfCardsByColor(deck.Cards, Color.YELLOW);
        }

        [TestMethod]
        public void TestDeckShuffle()
        {
            Deck deck = new Deck();
            deck.ShuffleDeck();
            PrintCardList(deck.Cards);
            Assert.IsTrue(deck.Cards.Count == 108);
            AssertNumberOfCardsByColor(deck.Cards, Color.RED);
            AssertNumberOfCardsByColor(deck.Cards, Color.BLUE);
            AssertNumberOfCardsByColor(deck.Cards, Color.GREEN);
            AssertNumberOfCardsByColor(deck.Cards, Color.YELLOW);
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

        private void AssertNumberOfCardsInList(List<Card> cards, int numCards, Face face, Color color)
        {
            var listOfCards = cards.Where(m => m.Face == face);
            var x = listOfCards.ToList();
            Assert.AreEqual(x.Count, numCards);
        }

        private void AssertNumberOfCardsByColor(List<Card> cards, Color color)
        {
            AssertNumberOfCardsInList(cards, 4, Face.ZERO, color);
            AssertNumberOfCardsInList(cards, 8, Face.ONE, color);
            AssertNumberOfCardsInList(cards, 8, Face.TWO, color);
            AssertNumberOfCardsInList(cards, 8, Face.THREE, color);
            AssertNumberOfCardsInList(cards, 8, Face.FOUR, color);
            AssertNumberOfCardsInList(cards, 8, Face.FIVE, color);
            AssertNumberOfCardsInList(cards, 8, Face.SIX, color);
            AssertNumberOfCardsInList(cards, 8, Face.SEVEN, color);
            AssertNumberOfCardsInList(cards, 8, Face.EIGHT, color);
            AssertNumberOfCardsInList(cards, 8, Face.NINE, color);
            AssertNumberOfCardsInList(cards, 8, Face.SKIP, color);
            AssertNumberOfCardsInList(cards, 8, Face.DRAW_TWO, color);
            AssertNumberOfCardsInList(cards, 8, Face.REVERSE, color);
        }
    }
}
