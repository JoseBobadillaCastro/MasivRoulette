using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasivRoulette.Models;
using MasivRoulette.Repositories;
using MasivRoulette.Util;
using Microsoft.Extensions.Configuration;
namespace MasivRoulette.Services
{
    public class RouletteService : IRouletteService
    {
        private IConfiguration _iConfiguration;
        private IRouletteRepository rouletteRepository;
        public RouletteService(IRouletteRepository rouletteRepository, IConfiguration iConfiguration)
        {
            this.rouletteRepository = rouletteRepository;
            _iConfiguration = iConfiguration;
        }
        public Roulette create()
        {
            Roulette roulette = new Roulette(){};
            return rouletteRepository.save(roulette);
        }
        public Roulette get(string id) 
        {
            Roulette roulette;
            roulette = rouletteRepository.get(id);
            return roulette;
        }
        public Roulette open(Roulette roulette)
        {
            if (roulette.isOpen == true)
            {
                throw new Exception("This roulette is already opened");
            }
            else if(roulette.winningNumber != null)
            {
                throw new Exception("This roulette has already been played");
            }
            else 
            {
                roulette.isOpen = true;
                roulette.bets = new List<Bet>();
            }
            return rouletteRepository.save(roulette);
        }
        public Roulette bet(string userId, string id, Bet bet)
        {
            Roulette roulette = rouletteRepository.get(id);
            if (roulette.isOpen == false)
            {
                throw new Exception("You can't bet on a closed roulette");
            }
            if (new BetValidation().validateBet(bet, Convert.ToDouble(_iConfiguration["Bets:maximum"].ToString())))
            {
                bet.id = Guid.NewGuid().ToString();
                bet.userId = userId;
                roulette.bets.Add(bet);
            }
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
