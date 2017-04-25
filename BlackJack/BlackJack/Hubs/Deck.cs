using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using BlackJack.Models;

namespace BlackJack.Hubs
{
    public class Deck
    {

        /*
          *Metodo che prende i mazzi di carte, li unisce e i mischia
          *   
          */
        public Card[] CreateDeck(int qtaMazzi)
        {
            Models.Deck[] decks = new Models.Deck[qtaMazzi];
            int qtaCarte = qtaMazzi * 52;
            Card[] cards = new Card[qtaCarte];
            for (int i = 0; i < qtaMazzi; i++)
            {

                cards = cards.Union(decks[i].Cards).ToArray();
            }
            return cards;
        }

        public void GetDeck()
        {
            Models.Deck[] decks = new Models.Deck[2];
        }

        public Card[] ShuffleCards(Card[] cards)
        {
            Random r = new Random();
            int randomIndex = 0;
            Card[] shuffleCards = new Card[cards.Length];
            var listCard = new List<Card>(cards);
            for (int i = 0; i < cards.Length; i++)
            {
                randomIndex = r.Next(0, cards.Length); //Choose a random object in the list
                shuffleCards[i] = listCard.ElementAt(randomIndex); //add it to the new, random list
                listCard.RemoveAt(randomIndex); //remove to avoid duplicates
            }
            return cards;
        }

        public string printCard(Card card)
        {
            return "Carta: " + card.rank + " | " + card.suit;
        }

        public int GetCardScore(Card card)
        {
            if (card.rank == 1)
            {
                return 11;
            }
            else if (card.rank >= 11)
            {
                return 10;
            }
            return card.rank;
        }
    }
}