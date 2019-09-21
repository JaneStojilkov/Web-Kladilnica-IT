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
        }
    }
}