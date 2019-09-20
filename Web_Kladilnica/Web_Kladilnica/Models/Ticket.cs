using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Kladilnica.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        public virtual IEnumerable<Game> games { get; set; }
        public List<int> guesses { get; set; }
        public float moneyInvested { get; set; }
        public float totalCoefficient {get
            {
                float total=0;
                foreach (Game g in games)
                {
                    if (g.selectedCoefficient == 1)
                    {
                        total += g.Coefficient1;
                    }
                    else if (g.selectedCoefficient == 2)
                    {
                        total += g.Coefficient2;
                    }
                    else
                    {
                        total += g.Coefficient3;
                    }
                }
                return total;
            }
        }
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
    }
}