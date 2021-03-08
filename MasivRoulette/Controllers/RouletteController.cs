using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasivRoulette.Services;
using MasivRoulette.Models;
namespace MasivRoulette.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouletteController : Controller
    {
        private IRouletteService rouletteService;
        public RouletteController(IRouletteService rouletteService) 
        {
            this.rouletteService = rouletteService;
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create()
        {
            Roulette roulette = rouletteService.create();
            return Ok(new
            {
                status = "OK",
                message = "Roulette created",
                detail = roulette
            });
        }
        [HttpPut]
        [Route("open/{id}")]
        public IActionResult Open(string id)
        {
            try
            {
                Roulette roulette = rouletteService.open(id);
                return Ok(new
                {
                    status = "OK",
                    message = "Roulette opened",
                    detail = roulette
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = "NOK",
                    message = "Invalid request",
                    detail = ex.Message
                });
            }
        }
        [HttpPost]
        [Route("bet/{id}")]
        public IActionResult Bet([FromHeader] string userId,string id,[FromBody] Bet bet)
        {
            try 
            {
                Roulette roulette = rouletteService.bet(userId, id, bet);
                return Ok(new
                {
                    status = "OK",
                    message = "Bet created",
                    detail = roulette
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = "NOK",
                    message = "Invalid request",
                    detail = ex.Message
                });
            }
        }
        [HttpPut]
        [Route("close/{id}")]
        public IActionResult Close(string id)
        {
            try
            {
                Roulette roulette = rouletteService.close(id);
                return Ok(new
                {
                    status = "OK",
                    message = "Roulette closed",
                    detail = roulette
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = "NOK",
                    message = "Invalid request",
                    detail = ex.Message
                });
            }
        }
        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            List<Roulette> roulettes = rouletteService.list();
            return Ok(new
            {
                status = "OK",
                message = "List of Roulettes",
                detail = roulettes
            });
        }
    }
}
