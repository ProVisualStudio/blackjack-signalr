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
        private static int conteggio = 0;
        private Card[] cards;
        private static int qtaMazzi = 6;
        private DeckHub dh = new DeckHub();
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
            Send();
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

       
        public void Send()
        {
            Clients.All.broadcast(conteggio);
        }



       
        public override Task OnDisconnected(bool stopCalled)
        {
            conteggio--;
            Send();
            return base.OnDisconnected(stopCalled);
        }

       
        
        /**
         * Metodo che prepara una nuova partita
         */       
        public void newGame()
        {
            this.cards = dh.CreateDeck(qtaMazzi);
            dh.ShuffleCards(this.cards);
            dh.printCards(cards);

        }

        public Card GetCard()
        {
            Card c = cards[pos];
            pos++;
            return c;
        }

    }
}