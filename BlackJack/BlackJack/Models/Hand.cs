using BlackJack.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Models
{
    public class Hand
    {
        DeckHelper dh = new DeckHelper();
        List<Card> cards = new List<Card>();
        public int Score { get; set; }
        public virtual User Utente { get; set; }

        public void AddCard(Card c)
        {
            cards.Add(c);
        }

       public int HandScore()
        {
            int ris = 0;
            for (int i = 0; i < cards.Count(); i++)
            {
                ris += dh.GetCardScore(cards.ElementAt(i));
            }
            return ris;
        }
    }
}