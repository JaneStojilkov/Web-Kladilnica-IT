using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Kladilnica.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        public List<Game> games { get; set; }
        public float moneyInvested { get; set; }
        public float totalCoefficient {get;set;}

        //TODO Ne biva taka sekojpat se kreira novi games so drugi selected koeficient brisi go selectedcoefficient pravi so guesses i so virtual list games ne so vakov posle bi trebalo da rabote
        public List<int> guesses { get; set; }
        public float WinMoney { get {
             return  moneyInvested* totalCoefficient;
             
            }
        }
        public bool win { get {
                foreach(Game g in games)
                {
                    if (!g.Completed || g.Result!=g.selectedCoefficient)
                    {
                        return false;
                    }
                }
                return true;
            } }
        public DateTime time { get; set; }
        public Ticket()
        {
            games = new List<Game>();
            time = DateTime.Now;
        }
    }
}