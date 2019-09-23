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
        public Boolean Completed { get {
                if ((int)DateTime.Now.Subtract(StartTime).TotalMinutes >= 105)
                    return true;
                return false;
            } }
        public int Time { get {

                return (int)DateTime.Now.Subtract(StartTime).TotalMinutes;
            } }
        public Boolean HalfTime { get {
                if (Time <= 45 || Time >= 60)
                    return false;
                return true;
            } }
        public string Sport { get; set; }
        public double Coefficient1 { get; set; }
        public double Coefficient2 { get; set; }
        public double Coefficient3 { get; set; }
        public string League { get; set; }
        public Team Team1 { get; set; }
        public int team1Score { get; set; }
        public int team2Score { get; set; }
        public Team Team2 { get; set; }
        public int Result
        {
            get
            {
                if (team1Score > team2Score)
                    return 1;
                else if (team1Score < team2Score)
                    return 2;
                return 0;
            }
        }
        public Game()
        {
        }
    }
}