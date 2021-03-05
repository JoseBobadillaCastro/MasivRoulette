using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasivRoulette.Models;
using MasivRoulette.Repositories;
namespace MasivRoulette.Services
{
    public class RouletteService : IRouletteService
    {
        private IRouletteRepository rouletteRepository;
        public RouletteService(IRouletteRepository rouletteRepository)
        {
            this.rouletteRepository = rouletteRepository;
        }
        public Roulette create()
        {
            Roulette roulette = new Roulette(){};
            return rouletteRepository.save(roulette);
        }
        public Roulette open(string id)
        {
            Roulette roulette = rouletteRepository.get(id);
            roulette.isOpen = true;
            roulette.bets = new List<Bet>();
            return rouletteRepository.save(roulette);
        }
        public Roulette bet(string userId, string id, Bet bet)
        {
            Roulette roulette = rouletteRepository.get(id);
            bet.id = Guid.NewGuid().ToString();
            bet.userId = userId;
            roulette.bets.Add(bet);
            return rouletteRepository.save(roulette);
        }
        public Roulette close(string id)
        {
            Roulette roulette = rouletteRepository.get(id);
            Random rnd = new Random();
            int winningNumer = rnd.Next(0, 36);
            string winningColor = (winningNumer % 2 == 0) ? "RED" : "BLACK";
            roulette.isOpen = false;
            roulette.winningNumber = winningNumer;
            roulette.gameDate = DateTime.UtcNow.Add(new TimeSpan(-5, 0, 0)).ToString("yyyy-MM-ddTHH:mm:ss") + "-05:00";
            foreach (Bet b in roulette.bets) 
            {
                if (b.selection == Convert.ToString(winningNumer) || b.selection.ToUpper() == winningColor)
                {
                    b.earnings = b.amount * b.type.payout;
                }
            }
            return rouletteRepository.save(roulette);
        }
        public List<Roulette> list()
        {
            return rouletteRepository.list();
        }
    }
}
