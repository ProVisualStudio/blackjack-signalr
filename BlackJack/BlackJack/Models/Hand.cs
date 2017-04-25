using BlackJack.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Models
{
    public class Hand
    {
        List<Card> cards = new List<Card>();
        public int Score { get; set; }

        public virtual User Utente { get; set; }

        public void AddCard(Card c)
        {
            cards.Add(c);
        }

       public int HandScore()
        {
            for (int i = 0; i < 2; i++)
            {

            }
            return 0;
        }
    }
}