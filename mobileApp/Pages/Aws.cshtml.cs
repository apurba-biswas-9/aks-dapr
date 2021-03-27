using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace mobileApp.Pages
{
    public class AwsModel : PageModel
    {
        private readonly ILogger<UserDetailsModel> _logger;
        private readonly DaprClient _dapr;

        public AwsModel(ILogger<UserDetailsModel> logger, DaprClient dapr)
        {
            _logger = logger;
            _dapr = dapr;
        }



        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public string _userId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //reading the value from redis
            var data = await _dapr.GetStateAsync<string>("statestorePostgresql", _userId);
            ViewData["AWSKey"] = $"token : '{data}' ";


            return Page();
        }
    }
}