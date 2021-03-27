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
    public class AzureModel : PageModel
    {
        private readonly ILogger<StateModel> _logger;
        private readonly DaprClient _dapr;

        public AzureModel(ILogger<StateModel> logger, DaprClient dapr)
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                await DaprUtility.Save(_dapr, "statestoreSql", _user);

                ViewData["AzureKey"] = $"token : '{_user.Key}' saved in cache ";
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

