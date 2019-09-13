using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Kladilnica.Models
{
    public class GameCreateModel
    {
        public Game game { get; set; }
        public List<Team> teams { get; set; }
        public GameCreateModel()
        {
            teams = new List<Team>();
            game = new Game();
        }
    }
}