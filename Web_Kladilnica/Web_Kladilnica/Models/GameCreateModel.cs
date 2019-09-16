using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Kladilnica.Models
{
    public class GameCreateModel
    {
        public Game game { get; set; }
        public List<Team> teams { get; set; }
        public int team1ID { get; set; }
        public int team2ID { get; set; }
        public IEnumerable<SelectListItem> selectTeams { get {
                return new SelectList(teams, "ID", "Name");
            } }
        public GameCreateModel()
        {
            teams = new List<Team>();
            game = new Game();
        }
    }
}