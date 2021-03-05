using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasivRoulette.Models
{
    [Serializable]
    public class Roulette
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public bool isOpen { get; set; } = false;
        public List<Bet> bets { get; set; }
        public int? winningNumber { get; set; }
        public string gameDate { get; set; }
    }
}
