using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Classes {
    public class Player {
        public Queue<Card> Deck { get; set; }

        public Player()
        {
            Deck = new Queue<Card>();
        }
    }
}