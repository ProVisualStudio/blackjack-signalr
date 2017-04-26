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
        public int Score { get; private set; }
        public virtual User Utente { get; set; }

        public Hand(string nome)
        {
            User u = new User();
            u.Nome = nome;
            Utente = u;
        }

        public void AddCard(Card c)
        {
            cards.Add(c);
        }

        public List<Card> GetCards()
        {
            return cards;
        }
        //Metodo che calcola il punteggio totale delle carte che ho in mano
        public int HandScore()
        {
            bool isAce = false;
            int ris = 0;
            for (int i = 0; i < cards.Count(); i++)
            {
                ris += dh.GetCardScore(cards.ElementAt(i));
                if (cards.ElementAt(i).Rank == 1) {
                    isAce = true;
                }
            }

            if(isAce = true && ris > 21)
            {
                ris = ris - 10;
            }
            Score =  ris;
            return ris;
        }


    }
}