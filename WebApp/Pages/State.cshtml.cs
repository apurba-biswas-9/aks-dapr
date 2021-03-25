using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Pages
{
    public class StateModel : PageModel
    {
        private readonly ILogger<StateModel> _logger;
        private readonly DaprClient _dapr;

        public StateModel(ILogger<StateModel> logger, DaprClient dapr)
        {
            _logger = logger;
            _dapr = dapr;
        }

       

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User _user{ get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //writing the value in redis
            var state = await _dapr.GetStateEntryAsync<string>("statestore", _user.Key);
            state.Value = _user.Value;
            await state.SaveAsync();

            //reading the value from redis
            var data = await _dapr.GetStateAsync<string>("statestore", _user.Key);
            ViewData["Key"] = $"token : '{_user.Key}' saved in cache ";


            return Page();
        }

    }
}
