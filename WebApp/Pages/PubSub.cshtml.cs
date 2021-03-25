using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Pages
{
    public class PubSubModel : PageModel
    {
        private readonly DaprClient _dapr;

        public PubSubModel(DaprClient dapr)
        {
            _dapr = dapr;
        }
        public IActionResult OnGet()
        {
            var staff = new List<NewsType>{
        new NewsType{ Id = 1, NewsCategory ="Sports"},
        new NewsType{ Id = 2,  NewsCategory ="Political"}
    };
            Staff = new SelectList(staff, nameof(NewsType.Id), nameof(NewsType.NewsCategory));

            return Page();
        }

        [BindProperty]
        public News _news { get; set; }

        public SelectList Staff { get; set; }
        [BindProperty]
        public int SelectedNewsType { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Console.WriteLine(SelectedNewsType);
            Console.WriteLine(_news.NewsContent);

            _news.NewsType = (SelectedNewsType == 1) ? "sports" : "political";


            await _dapr.PublishEventAsync("pubsub", _news.NewsType, new News { NewsType = _news.NewsType, NewsContent = _news.NewsContent });


            ViewData["msg"] = "message sent successfully";

            var staff = new List<NewsType>{
        new NewsType{ Id = 1, NewsCategory ="Sports"},
        new NewsType{ Id = 2,  NewsCategory ="Political"}
    };
            Staff = new SelectList(staff, nameof(NewsType.Id), nameof(NewsType.NewsCategory));

            return Page();
        }
    }

    public class NewsType
    {
        public int Id { get; set; }
        public string NewsCategory { get; set; }

    }
}
