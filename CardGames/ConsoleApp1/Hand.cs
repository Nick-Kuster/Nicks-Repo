using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Hand
    {
        public List<Card> Cards { get; set; }
        public bool Uno => Cards.Count == 1;
        public Hand(List<Card> cardsDealt)
        {
            Cards = cardsDealt;
        }
        public void AddCards(List<Card> cards)
        {
            Cards.AddRange(cards);
        }
    }
}
