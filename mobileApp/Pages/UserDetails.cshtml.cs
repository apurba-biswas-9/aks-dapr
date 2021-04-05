using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace mobileApp.Pages
{
    public class UserDetailsModel : PageModel
    {
        private readonly ILogger<UserDetailsModel> _logger;
        private readonly DaprClient _dapr;

        public UserDetailsModel(ILogger<UserDetailsModel> logger, DaprClient dapr)
        {
            _logger = logger;
            _dapr = dapr;
        }



        public IActionResult OnGet()
        {
            GetStates();
            return Page();
        }

        public SelectList StateLst { get; set; }
        [BindProperty]
        public int SelectedStateType { get; set; }

        [BindProperty]
        public string _userId { get; set; }

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

                //reading the value from redis
                var data = await _dapr.GetStateAsync<string>(type, _userId);
                ViewData["Key"] = $"User: {_userId} | Token : {data} ";
            }
            catch (Exception ex)
            {
                Console.WriteLine("------------System log--------------");
                Console.WriteLine(ex);
            }

            GetStates();

            //this.SelectedStateType = 0;
            this._userId = string.Empty;

            this.ModelState.Clear();

           // return Redirect("~/Index");
          

             return Page();
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