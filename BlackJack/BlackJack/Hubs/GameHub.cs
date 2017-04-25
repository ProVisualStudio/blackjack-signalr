using System;
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
        private Deck dh = new Deck();
        private int pos = 0;

        public override Task OnConnected()
        {
            foreach (var u in Users)
            {
                Clients.Caller.newNickName(u.Nome);
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
            Clients.All.newNickName(name);
        }

      
       
        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

       
        
        /**
         * Metodo che prepara una nuova partita
         */       
        public void newGame()
        {
            this.cards = dh.CreateDeck(qtaMazzi);
            dh.ShuffleCards(this.cards);
            foreach (Card card in cards)
            {
                Clients.All.printCard("Carta: " + card.rank + " | " + card.suit);
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

        public bool Hit()
        {

            return true;
        }

    }
}