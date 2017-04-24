using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace BlackJack.Hubs
{
    public class User
    {
        public string ConnectionId { get; set; }
        public string Nome { get; set; }
        /**
         * variabile che conta quanti punti / soldi abbiamo a disposizione per giocare 
         */
        public int Money { get; set; }

    }
}