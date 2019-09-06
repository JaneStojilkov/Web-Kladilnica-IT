using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Kladilnica.Models
{
    public class Game
    {

        public int ID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Boolean Completed { get; set; }
        public float Time { get; set; }
        public Boolean HalfTime { get; set; }
        public string Sport { get; set; }
        public float Coefficient1 { get; set; }
        public float Coefficient2 { get; set; }
        public float Coefficient3 { get; set; }
        public string League { get; set; }
        public int selectedCoefficient { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public int Result
        {
            get
            {
                if (Team1.score > Team2.score)
                    return 1;
                else if (Team1.score < Team2.score)
                    return 2;
                return 0;
            }
        }
    }
}