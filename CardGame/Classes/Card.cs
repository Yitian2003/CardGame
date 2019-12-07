using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace CardGame.Classes {
    public enum Suit {
        Clubs,
        Diamonds,
        Spades,
        Hearts
    }
    public class Card {
        public string DisplayName { get; set; }
        public Suit Suit { get; set; }
        public int Value { get; set; }
        
    }
}