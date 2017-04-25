﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using BlackJack.Models;

namespace BlackJack.Hubs
{
    public class GameHub : Hub
    {
        private static List<User> Users = new List<User>();
        private static Table table = new Table();
        private Card[] cards;
        private static int qtaMazzi = 6;
        private DeckHelper dh = new DeckHelper();
        private int pos = 0;
        private Hand dealerH;
        private Hand playerH;
        private Random rnd;
        private bool isPlayerBust = false;
        private bool isDealerBust = false;

        public override Task OnConnected()
        {
            foreach (var u in Users)
            {
                Clients.Caller.newNickName(Context.ConnectionId, u.Nome);
            }
            User user = new User();
            user.ConnectionId = Context.ConnectionId;
            Users.Add(user);
            return base.OnConnected();
        }

        public void SetNickname(string name)
        {
            User me = (from u in Users
                       where u.ConnectionId == Context.ConnectionId
                       select u).First();
            me.Nome = name;
            Clients.All.newNickName(Context.ConnectionId, name);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            User me = (from u in Users
                       where u.ConnectionId == Context.ConnectionId
                       select u).First();
            Users.Remove(me);
            Clients.All.updNickName();
            foreach (var u in Users)
            {
                Clients.All.newNickName(Context.ConnectionId, u.Nome);
            }
            return base.OnDisconnected(stopCalled);
        }

       public GameHub()
        {
            rnd = new Random();
        }
        
        /**
         * Metodo che prepara una nuova partita
         */       
        public void NewGame()
        {
            this.cards = dh.CreateDeck(qtaMazzi);
            this.cards = this.cards.Skip(1).ToArray();
            this.cards = dh.ShuffleCards(this.cards);
            dealerH= new Hand();
            playerH = new Hand();
            for (int i = 0; i < cards.Length; i++)
            {
                Clients.Caller.printCard("ID:"+ i + " " + cards[i].Rank.ToString()+" "+ cards[i].Suit.ToString() + "Val:" + dh.GetCardScore(cards[i]));
            }
        }
        /**
         * Metodo ce fa terminare una partita e ritorna il vincitore
         */
        public Hand EndGame()
        {
            dealerH.HandScore();
            playerH.HandScore();

            if (isPlayerBust)
            {
                return dealerH;
            }
            else if (isDealerBust)
            {
                return playerH;
            }
            else if(playerH.Score > dealerH.Score && playerH.Score <= 21)
            {
                return playerH;
            }
            else
            {
                return dealerH;
            }
        }

        public Card GetCard()
        {
            if(pos > cards.Length)
            {
                dh.ShuffleCards(cards);
                pos = 0;
            }
            Card c = cards[pos];
            pos++;
            return c;
        }

       
        public void Hit()
        {
            playerH.AddCard(GetCard());
            playerH.HandScore();
            if (!BustCheck(playerH))
            {
                //chiedere al utente cosa fare
            }
            else
            {
                isPlayerBust = true;
                EndGame();
            }
            
        }

        //metodo che effettua il controllo se l'utente a fa terminare il turno al player
        public void Stay()
        {
            playerH.HandScore();
            DealerTurn();
            
        }
        /**
         *metodo che simula il più fedelmente possibile il "funzionamento" del dealer 
         * */
        public void DealerTurn() {
            dealerH.HandScore();
            if (dealerH.Score < 17)
            {
                dealerH.AddCard(GetCard());
                if (!BustCheck(dealerH))
                {
                    DealerTurn();
                }
                else
                {
                    isDealerBust = true;
                    EndGame();
                }
            }
            else if(dealerH.Score > 20) 
            {
                EndGame();
            }
            else
            {
                if (rnd.Next(0, 2) == 0)
                {
                    dealerH.AddCard(GetCard());
                    EndGame();
                }
                else
                {
                    EndGame();
                }
            }
            
        }

        //Metodo che controlla se nella manno si ha Sballato (Bust), in modo da far terminare il gioco
        public bool BustCheck(Hand h)
        {
            if(h.Score > 21)
            {
                EndGame();
                return true;
            }
            return false;
        }

    }
}