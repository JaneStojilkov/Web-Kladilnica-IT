using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Kladilnica.Models
{
    public class TicketListGamesView
    {
        List<Ticket> tickets { get; set; }
        List<List<Game>> games { get; set; }
        TicketListGamesView()
        {

        }
    }
}