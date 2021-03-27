using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IServiceApi _service;

        public IndexModel(IServiceApi service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var data = await _service.Gets();
                ViewData["WeatherForecastData"] = data;
            }
            catch (Exception ex) 
            {
                Console.WriteLine("------------System log--------------");
                Console.WriteLine(ex);
            }

            return Page();
        }
    }
}
