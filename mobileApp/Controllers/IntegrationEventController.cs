using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobileApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationEventController : ControllerBase
    {
        private readonly DaprClient _dapr;
        private IMemoryCache _cache;
        readonly CustomerFeatures _customerFeature;

        public IntegrationEventController(DaprClient dapr, IMemoryCache memoryCache)
        {
            _dapr = dapr;
            _cache = memoryCache;
        }      

        [HttpPost("/subssport")]
        [Topic("pubsub", "sports")]
        public async Task SubsCribeSport(News news)
        {
            var lst = _cache.Get(news.NewsType);

            if (lst != null)
            {
                var dta = (List<string>)lst;
                dta.Add(news.NewsContent);
                _cache.Set(news.NewsType, dta);
            }
            else 
            {
                _cache.Set(news.NewsType, new List<string> { news.NewsContent });
            }
        }


        [HttpPost("/subsPolitical")]
        [Topic("pubsub", "political")]
        public async Task SubsCriberPolitical(News news)
        {
            var lst = _cache.Get(news.NewsType);

            if (lst != null)
            {
                var dta = (List<string>)lst;
                dta.Add(news.NewsContent);
                _cache.Set(news.NewsType, dta);
            }
            else
            {
                _cache.Set(news.NewsType, new List<string> { news.NewsContent });
            }
        }
    }
}
