using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Pages
{
    public class AWSModel : PageModel
    {
        private readonly ILogger<StateModel> _logger;
        private readonly DaprClient _dapr;

        public AWSModel(ILogger<StateModel> logger, DaprClient dapr)
        {
            _logger = logger;
            _dapr = dapr;
        }



        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User _user { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await DaprUtility.Save(_dapr, "AWSstatestore", _user);

            ViewData["AWSKey"] = $"token : '{_user.Key}' saved in cache ";


            return Page();
        }

    }
}


