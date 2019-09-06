using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Web_Kladilnica.Models
{
    public class DBContext :DbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }

        public System.Data.Entity.DbSet<Web_Kladilnica.Models.Game> Games { get; set; }

        public System.Data.Entity.DbSet<Web_Kladilnica.Models.Betting_Ticket> Betting_Ticket { get; set; }
    }
}