using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Kladilnica.Models
{
    public class TicketCreateModel
    {
        public string[] gameIds { get; set; }
        public int[] guesses { get; set; }
        public int moneyInvested { get; set; }
        public List<GameViewModelT> games { get; set; }
        public double totalCoefficient { get; set; }
        public TicketCreateModel()
        {
            games = new List<GameViewModelT>();
        }
    }
}