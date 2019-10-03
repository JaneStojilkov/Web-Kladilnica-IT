using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Kladilnica.Models
{
    public class TicketDisplayViewModel
    {
        public int TicketID { get; set; }
        [Display(Name ="Total Coefficient")]
        public double totalCoefficient { get; set; }
        public List<GameViewModelT> games { get; set; }
        [Display(Name="Invested Money")]

        public int moneyInvested { get; set; }
        public DateTime Time { get; set; }
        public int[] guesses { get; set; }
        [Display(Name ="Future Winnings")]
        public double futureWinnings { get; set; }
        [Display(Name ="Winning Ticket")]
        public bool Result { get; set; }
        public TicketDisplayViewModel()
        {
            games = new List<GameViewModelT>();
        }
    }
}