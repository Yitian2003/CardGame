using CardGame.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace CardGame {
    public partial class _Default : Page {

        public Player Computer;
        public Player You;
        public string Action = "";
        public Queue<Card> Cards = DeckCreator.CreateCards();
        public string Winner = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Computer"] == null || Session["You"] == null)
            {
                Computer = new Player();
                You = new Player();
                Session["You"] = You;
                Session["Computer"] = Computer;
                Session["Cards"] = Cards;
            }           
        }
    }
}