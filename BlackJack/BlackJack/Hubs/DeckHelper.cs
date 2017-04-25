using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using BlackJack.Models;

namespace BlackJack.Hubs
{
    public class DeckHelper
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
                decks[i] = new Models.Deck();
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
            List<Card> listCard = cards.OfType<Card>().ToList();
            for (int i = 0; i < shuffleCards.Length; i++)
            {

                randomIndex = r.Next(0, listCard.Count); //Choose a random object in the list
                if(listCard.ElementAtOrDefault(randomIndex) != null)
                {
                    if (randomIndex != 0)
                    {
                        shuffleCards[i] = listCard.ElementAt(randomIndex - 1); //add it to the new, random list
                        listCard.RemoveAt(randomIndex - 1); //remove to avoid duplicates
                    }
                    else
                    {
                        shuffleCards[i] = listCard.ElementAt(randomIndex); //add it to the new, random list
                        listCard.RemoveAt(randomIndex); //remove to avoid duplicates
                    }
                }
            }
            return shuffleCards;
        }


        public int GetCardScore(Card card)
        {
            if (card.Rank == 1)
            {
                return 11;
            }
            else if (card.Rank >= 11)
            {
                return 10;
            }
            return card.Rank;
        }
    }
}