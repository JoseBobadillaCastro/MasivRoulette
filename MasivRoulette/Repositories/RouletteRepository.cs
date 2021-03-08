using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using MasivRoulette.Models;
using Newtonsoft.Json;
using System.Text;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
namespace MasivRoulette.Repositories
{
    public class RouletteRepository : IRouletteRepository
    {
        private readonly IDistributedCache distributedCache;
        private readonly DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(10))
            .SetAbsoluteExpiration(DateTime.Now.AddHours(12));
        private IConfiguration _iConfiguration;
        public RouletteRepository(IDistributedCache distributedCache, IConfiguration iConfiguration)
        {
            this.distributedCache = distributedCache;
            _iConfiguration = iConfiguration;
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
            string serializedRoulette = "";
            if (encodedRoulette == null)
            {
                throw new Exception("Invalid roulette id");
            }
            else
            {
                serializedRoulette = Encoding.UTF8.GetString(encodedRoulette);
            }
            return JsonConvert.DeserializeObject<Roulette>(serializedRoulette);
        }
        public List<Roulette> list()
        {
            List<Roulette> roulettes = new List<Roulette>();
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_iConfiguration.GetValue<string>("Redis:ConnectionString"));
            IServer server = redis.GetServer(_iConfiguration.GetValue<string>("Redis:host"), Convert.ToInt32(_iConfiguration.GetValue<string>("Redis:port")));
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
