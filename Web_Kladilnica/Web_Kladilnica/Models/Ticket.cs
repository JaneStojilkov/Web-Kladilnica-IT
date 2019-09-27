using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web_Kladilnica.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        public int[] gameIDs
        {
            get
            {
                return Array.ConvertAll(InternalgameIDs.Split(';'), int.Parse);
            }
            set
            {
                var _data = value;
                InternalgameIDs= String.Join(";", _data.Select(p => p.ToString()).ToArray());

            }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalGuesses { get; set; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalgameIDs { get; set; }
        public int moneyInvested { get; set; }
        public double totalCoefficient {get;set;}
        public int[] guesses
        {
            get
            {
                return Array.ConvertAll(InternalGuesses.Split(';'), int.Parse);
            }
            set
            {
                var _data = value;
                InternalGuesses = String.Join(";", _data.Select(p => p.ToString()).ToArray());

            }
        }
        public double WinMoney { get {
             return  moneyInvested* totalCoefficient;
             
            }
        }
        public bool win {
            get;set;
        }
        public DateTime time { get; set; }
        public Ticket()
        {
            win = false;
        }
    }
}