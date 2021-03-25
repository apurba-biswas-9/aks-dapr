using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace mobileApp.Pages
{
    public class PoliticalModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IMemoryCache _cache;

        public PoliticalModel(ILogger<IndexModel> logger, IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public void OnGet()
        {

            var lst = _cache.Get("political");
            if (lst != null)
            {
                var dta = (List<string>)lst;
                if (dta?.Count > 0)
                {
                    ViewData["political"] = dta;
                }

                //    if (keys != null)
                //{
                //    keys.ToList().ForEach(x => 
                //    {
                //        ViewData["WeatherForecastData"] = ViewData["WeatherForecastData"] + Environment.NewLine + HttpContext.Session.GetString(x);
                //    });
            }
        }
    }
}
