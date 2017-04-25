using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Models
{
    public class Deck
    {
        //public virtual ICollection<Card> Cards { get; set; }

        /*
         * http://stackoverflow.com/questions/33028678/creating-a-simple-deck-of-cards-c-sharp
         */
        //private Card[] cards;
        private Card[] cards;

        public Card[] Cards
        {
            get { return cards; }
            private set { cards = value; }
        }


        public Deck()
    {
        Cards = new Card[52];
        var index = 0;

        foreach (var suit in new[] { "Spades", "Hearts", "Clubs", "Diamonds", })
        {
            for (var rank = 0; rank < 13; rank++)
            {
                  Cards[index++] = new Card(rank, suit);
            }
        }
    }

}
}
 