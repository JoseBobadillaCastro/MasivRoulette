using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasivRoulette.Models
{
    public class BetType
    {
        public string name { get; set; }
        public double payout { get; set; }
        public BetType(string name) 
        {
            this.name = name;
            switch (this.name) 
            {
                case "number":
                    payout = 5;
                    break;
                case "color":
                    payout = 1.8;
                    break;
            }
        }
    }
}
