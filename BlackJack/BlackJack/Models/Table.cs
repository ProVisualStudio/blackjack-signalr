using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace BlackJack.Hubs
{
    public class Table
    {
        public virtual ICollection<User> Users { get; set; }
    }
}