using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using MasivRoulette.Models;
using Newtonsoft.Json;
using System.Text;
using StackExchange.Redis;
namespace MasivRoulette.Repositories
{
    public class RouletteRepository : IRouletteRepository
    {
        private readonly IDistributedCache distributedCache;
        private readonly DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5))
            .SetAbsoluteExpiration(DateTime.Now.AddHours(6));
        public RouletteRepository(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }
        public Roulette save(Roulette roulette)
        {
            string serializedRoulette = JsonConvert.SerializeObject(roulette);
            var encodedRoulette = Encoding.UTF8.GetBytes(serializedRoulette);
            distributedCache.Set(roulette.id, encodedRoulette, options);
            return roulette;
        }
        public Roulette get(string id)
        {
            var encodedRoulette = distributedCache.Get(id);
            string serializedRoulette = Encoding.UTF8.GetString(encodedRoulette);
            return JsonConvert.DeserializeObject<Roulette>(serializedRoulette);
        }
        public List<Roulette> list()
        {
            List<Roulette> roulettes = new List<Roulette>();
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
            IServer server = redis.GetServer("localhost", 6379);
            var keys = server.Keys();
            foreach (RedisKey k in keys) 
            {
                Roulette r = get(k);
                roulettes.Add(r);
            }
            return roulettes;
        }
    }
}
