using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Kladilnica.Models
{
    public class TicketGamesView
    {
        public Ticket ticket { get; set; }
        public List<Game> games { get; set; }
        public TicketGamesView()
        {
            games = new List<Game>();
        }
        public string ids { get; set; }
        public string gameCoef { get; set; }
    }
}