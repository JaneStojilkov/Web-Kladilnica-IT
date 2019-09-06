using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Kladilnica.Models
{
    public class Game
    {
        enum Type { x = 0, HomeWin = 1, GuestWin = 2 };
        public float Coefficient { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Boolean Completed { get; set; }
        public float Time { get; set; }
        public Boolean HalfTime { get; set; }
        public int ID { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string League { get; set; }
    }
}