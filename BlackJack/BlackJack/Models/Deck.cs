using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Models
{
    public class Deck
    {
        public virtual ICollection<Card> Cards { get; set; }
    }
}