using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Kladilnica.Models
{
    public class Betting_Ticket
    {
        public List<Game> Games = new List<Game>();
        public float Invested { get; set; }
        public float SumCooef { get; set; }
        public float SumWin { get; set; }
        public int ID { get; set; }
        public Boolean Won { get; set; }
        public DateTime Time { get; set; }
       // public ApplicationUser User { get; set; }

    }
}