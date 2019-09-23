using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Kladilnica.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        public virtual List<Game> games { get; set; }
        public int moneyInvested { get; set; }
        public double totalCoefficient {get;set;}

        //TODO Ne biva taka sekojpat se kreira novi games so drugi selected koeficient brisi go selectedcoefficient pravi so guesses i so virtual list games ne so vakov posle bi trebalo da rabote
        public int[] guesses { get; set; }
        public double WinMoney { get {
             return  moneyInvested* totalCoefficient;
             
            }
        }
        public bool win
        {
            get
            {
                if (games != null && guesses != null)
                {
                    for (int i = 0; i < games.Count && i<guesses.Length; i++)
                    {
                        if (!games[i].Completed || games[i].Result != guesses[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
          
        }
        public DateTime time { get; set; }
        public Ticket()
        {
            games = new List<Game>();
            time = DateTime.Now;
                
        }
    }
}