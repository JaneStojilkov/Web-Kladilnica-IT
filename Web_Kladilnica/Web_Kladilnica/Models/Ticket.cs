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
        public float totalCoefficient { get; set; }
        public float WinMoney { get; set; }
        public bool win { get; set; }
        public DateTime time { get; set; }
    }
}