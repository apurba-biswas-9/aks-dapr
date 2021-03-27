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
        private readonly ILogger<PoliticalModel> _logger;
        private IMemoryCache _cache;

        public PoliticalModel(ILogger<PoliticalModel> logger, IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public void OnGet()
        {
            try
            {
                var lst = _cache.Get("political");
                if (lst != null)
                {
                    var dta = (List<string>)lst;
                    if (dta?.Count > 0)
                    {
                        ViewData["political"] = dta;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("------------System log--------------");
                Console.WriteLine(ex);
            }
        }
    }
}
