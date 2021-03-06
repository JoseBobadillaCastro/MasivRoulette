using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace MasivRoulette.Models
{
    [Serializable]
    public class Bet
    {
        public string id { get; set; }
        public string userId { get; set; }
        public double amount { get; set; }
        public string selection { get; set; } 
        public BetType type { get; set; }
        public double earnings { get; set; } = 0;
    }
}
