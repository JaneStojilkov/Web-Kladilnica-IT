using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Kladilnica.Models
{
    public class GameViewModelT
    {
        
        public int team1Score { get; set; }
        public int team2Score { get; set; }
        [Display(Name = "Team 1")]
        public Team team1 { get; set; }
        [Display(Name = "Team 2")]
        public Team team2 { get; set; }
        [Display(Name ="Completed")]
        public bool completed { get; set; }
        [Display(Name = "Coefficient")]
        public double selCoefficient { get; set; }
        public int Time { get; set; }
        [Display(Name ="Start Time")]
        public DateTime start { get; set; }
        [Display(Name = "End Time")]
        public DateTime end { get; set; }
        [Display(Name = "Result")]
        public int outcome { get; set; }
    }
}