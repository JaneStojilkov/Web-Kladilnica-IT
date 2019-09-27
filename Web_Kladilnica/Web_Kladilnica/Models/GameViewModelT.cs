using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Kladilnica.Models
{
    public class GameViewModelT
    {
        public int gameId { get; set; }
        public int team1Score { get; set; }
        public int team2Score { get; set; }
        public bool HalfTime { get; set; }
        [Display(Name = "Team 1")]
        public Team team1 { get; set; }
        [Display(Name = "Team 2")]
        public Team team2 { get; set; }
        [Display(Name ="Completed")]
        public bool completed { get; set; }
        [Display(Name = "Coefficient 1")]
        public double Coefficient1 { get; set; }
        [Display(Name = "Coefficient 2")]
        public double Coefficient2 { get; set; }
        [Display(Name = "Coefficient 3")]
        public double Coefficient3 { get; set; }
        public int Time { get; set; }
        [Display(Name ="Start Time")]
        public DateTime start { get; set; }
        [Display(Name = "End Time")]
        public DateTime end { get; set; }
        [Display(Name = "Result")]
        public int outcome { get; set; }
    }
}