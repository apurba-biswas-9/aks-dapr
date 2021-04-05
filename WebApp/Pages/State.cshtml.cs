using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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


        public SelectList StateLst { get; set; }
        [BindProperty]
        public int SelectedStateType { get; set; }



        public IActionResult OnGet()
        {
            GetStates();
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

                var type = string.Empty;
                if (SelectedStateType is 1)
                    type = "statestore";
                else if (SelectedStateType is 2)
                    type = "statestorePostgresql";
                else
                    type = "statestoreSql";

                await DaprUtility.Save(_dapr, type , _user);
                ViewData["Key"] = $" Saved User Token ";
            }
            catch (Exception ex)
            {
                Console.WriteLine("------------System log--------------");
                Console.WriteLine(ex);
            }

            GetStates();

         
            this._user.Key = string.Empty;
            this._user.Value = string.Empty;
            this.ModelState.Clear();

          return  RedirectToPage("Message", new {  msg = ViewData["Key"].ToString() } );
            
        }

        public void GetStates()
        {
            var staff = new List<StateType>{
                 new StateType{ Id = 1, NewsCategory ="Redis"},
                 new StateType{ Id = 2,  NewsCategory ="PostgreSQL"},
                 new StateType{ Id = 3,  NewsCategory ="Sql Server"}
            };
            StateLst = new SelectList(staff, nameof(StateType.Id), nameof(StateType.NewsCategory));
        }

    }

    public class StateType
    {
        public int Id { get; set; }
        public string NewsCategory { get; set; }

    }
}
