using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasivRoulette.Models;
namespace MasivRoulette.Services
{
    public interface IRouletteService
    {
        public Roulette create();
        public Roulette open(string id);
        public Roulette bet(string userId, string id, Bet bet);
        public Roulette close(string id);
        public List<Roulette> list();
    }
}
