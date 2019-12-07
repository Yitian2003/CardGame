using CardGame.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace CardGame.Services {
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService {

        [WebMethod(enableSession: true)]
        public string HitMe()
        {
            Player You = (Player)HttpContext.Current.Session["You"];
            Player Computer = (Player)HttpContext.Current.Session["Computer"];
            Queue<Card> Cards = (Queue<Card>)HttpContext.Current.Session["Cards"];
            string html = "";

            // Add a card to "You"'s list
            You.Deck.Enqueue(Cards.Dequeue());
            HttpContext.Current.Session["You"] = You;
            if (SumValue(Computer) < 18)
            {
                // Add a card to "Computer"'s list when all the cards sum of value less than 18
                Computer.Deck.Enqueue(Cards.Dequeue());
                HttpContext.Current.Session["Computer"] = Computer;
            }
            HttpContext.Current.Session["Cards"] = Cards;

            html = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/GameTable.html"));
            html = ReplaceSign(html, "you", You);
            html = ReplaceSign(html, "computer", Computer);

            html = ComparePlayers(html, You, Computer);            

            return html;
        }

        // Compare 2 players, if any one's cards sum of value over 21, lost. equal 21 win.
        private string ComparePlayers(string html, Player you, Player computer)
        {
            if (SumValue(you) < 21 && SumValue(computer) < 21)
            {
                html = HtmlResult(html, "");
            } else if (SumValue(you) > 21 || SumValue(computer) == 21)
            {
                html = HtmlResult(html, "You Lost");
            } else if (SumValue(computer) > 21 || SumValue(you) == 21)
            {
                html = HtmlResult(html, "You Win");
            }
            return html;
        }
        
        //Replace card holder with img with card link.
        private string ReplaceSign(string html, string playerName, Player player)
        {
            if(player != null)
            {
                List<Card> transformedCards = player.Deck.ToList();
                for (int i = 0; i < 10; i++)
                {
                    if (i < transformedCards.Count)
                    {
                        html = html.Replace("~~" + playerName + "_" + i + "~~", "<img style='width: 100px; height: 160px;' src='/images/" + transformedCards[i].DisplayName + ".png'/>");
                    } else
                    {
                        html = html.Replace("~~" + playerName + "_" + i + "~~", "");
                    }
                }
            } else
            {
                for (int i = 0; i < 10; i++)
                {
                     html = html.Replace("~~" + playerName + "_" + i + "~~", "");
                }
            }
            
            return html;
        }
        

        // Get the sum of all the cards value
        private int SumValue(Player player)
        {
            int total = 0;
            List<Card> transformedCards = player.Deck.ToList();
            for (int i = 0; i < transformedCards.Count; i++)
            {
                if ((transformedCards[i].Value == 14 && total <= 10) || transformedCards[i].Value != 14)
                {
                    total += ConvertCardValue(transformedCards[i].Value);
                } else
                {
                    total += 1;
                }

            }
            return total;
        }
        private int ConvertCardValue(int card_value)
        {
            if (card_value >= 2 && card_value <= 10)
            {
                return card_value;
            } else if (card_value >= 11 && card_value <= 13)
            {
                return 10;
            } else
            {
                return 11;
            }
        }

        [WebMethod(enableSession: true)]
        public string NewGame()
        {
            HttpContext _Context = HttpContext.Current;

            Player Computer = new Player();
            Player You = new Player();
            HttpContext.Current.Session["You"] = You;
            HttpContext.Current.Session["Computer"] = Computer;

            var html = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/GameTable.html"));
            html = ReplaceSign(html, "you", null);
            html = ReplaceSign(html, "computer", null);

            html = html.Replace("~~display_result~~", "hide");
            html = html.Replace("~~display_hit~~", "");
            html = html.Replace("~~display_pass~~", "hide");

            return html;
        }

        [WebMethod(enableSession: true)]
        public string Pass()
        {
            HttpContext _Context = HttpContext.Current;
            Player You = (Player)HttpContext.Current.Session["You"];
            Player Computer = (Player)HttpContext.Current.Session["Computer"];
            Queue<Card> Cards = (Queue<Card>)HttpContext.Current.Session["Cards"];
            string html = "";
            html = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/GameTable.html"));

            if (SumValue(Computer) < 18)
            {
                Computer.Deck.Enqueue(Cards.Dequeue());
                HttpContext.Current.Session["Computer"] = Computer;
                HttpContext.Current.Session["Cards"] = Cards;
                
                html = ReplaceSign(html, "you", You);
                html = ReplaceSign(html, "computer", Computer);
            } else
            {
                html = ReplaceSign(html, "you", You);
                html = ReplaceSign(html, "computer", Computer);

                if (SumValue(You) > SumValue(Computer))
                {
                    html = HtmlResult(html, "You Win");
                } else if(SumValue(You) < SumValue(Computer))
                {
                    html = HtmlResult(html, "You Lost");
                } else
                {
                    html = HtmlResult(html, "Tie");
                }
            }

            html = ComparePlayers(html, You, Computer);

            return html;
        }

        private string HtmlResult(string html, string result)
        {
            if(result != "")
            {
                html = html.Replace("~~display_result~~", "");
                html = html.Replace("~~result~~", result);
                html = html.Replace("~~display_hit~~", "hide");
                html = html.Replace("~~display_pass~~", "hide");
            } else
            {
                html = html.Replace("~~display_result~~", "hide");
                html = html.Replace("~~display_hit~~", "");
                html = html.Replace("~~display_pass~~", "");
            }        
            return html;
        }
    }
}
