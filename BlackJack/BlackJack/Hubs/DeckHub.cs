using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace BlackJack.Hubs
{
    public class DeckHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void generateDeck()
        {

        }
    }
}