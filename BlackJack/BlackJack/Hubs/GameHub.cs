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
        private DeckHelper dh = new DeckHelper();
        private int pos = 0;
        private Hand dealerH;
        private Hand playerH;

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

        public bool Hit(Hand player)
        {
            
            return true;
        }

    }
}