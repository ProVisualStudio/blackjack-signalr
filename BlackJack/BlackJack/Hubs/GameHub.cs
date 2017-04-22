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
        private Deck[] decks;
        private Card[] cards;
        private static int qtaMazzi = 6;

        public override Task OnConnected()
        {
            foreach (var u in Users)
            {
                Clients.Caller.newNickName(u.Nome);
            }
            User user = new User();
            user.ConnectionId = Context.ConnectionId;
            Users.Add(user);
            conteggio++;
            if(conteggio <= 7)
            {
                table.Users.Add(user);
            }
            Send();
            return base.OnConnected();
        }

        public void SetNickname(string name)
        {
            User me = (from u in Users
                       where u.ConnectionId == Context.ConnectionId
                       select u).First();
            me.Nome = name;
            Clients.Others.newNickName(name);
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

        public void OnGameLoaded()
        {

        }
        /*
         *Metodo che prende i mazzi di carte, li unisce e i mischia (da finire)
         *   
             */
        public Card[] MischiaCarte() {
            decks = new Deck[qtaMazzi];
            int qtaCarte = qtaMazzi * 52;
            cards = new Card[qtaCarte];
            for (int i = 0; i < qtaMazzi; i++)
            {
                cards = decks[qtaMazzi].Cards;
            }
            return cards;
        }
    }
}