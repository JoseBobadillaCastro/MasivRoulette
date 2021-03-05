using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasivRoulette.Models;
namespace MasivRoulette.Repositories
{
    public interface IRouletteRepository
    {
        public Roulette save(Roulette roulette);
        public Roulette get(string id);
        public List<Roulette> list();
    }
}
