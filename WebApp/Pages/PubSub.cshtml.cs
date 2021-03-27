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
            GetNewsType();
            GetMessageBroker();

            return Page();
        }

        [BindProperty]
        public News _news { get; set; }

        public SelectList Staff { get; set; }

        public SelectList MB { get; set; }
        [BindProperty]
        public int SelectedNewsType { get; set; }

        [BindProperty]
        public int SelectedMB { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                Console.WriteLine(SelectedNewsType);
                Console.WriteLine(SelectedMB);
                Console.WriteLine(_news.NewsContent);



                _news.NewsType = (SelectedNewsType == 1) ? "sports" : "political";

                var mbType = (SelectedMB == 1) ? "pubsubredis" : "pubsub";

                _news.NewsContent = (SelectedMB == 1) ? "Redis----> " + _news.NewsContent : "RabittMQ----> " + _news.NewsContent;

                await _dapr.PublishEventAsync(mbType, _news.NewsType, new News { NewsType = _news.NewsType, NewsContent =   _news.NewsContent });


                ViewData["msg"] = "message sent successfully";

                GetNewsType();
                GetMessageBroker();

            }
            catch (Exception ex)
            {
                Console.WriteLine("------------System log--------------");
                Console.WriteLine(ex);
            }

            return Page();
        }

        public void GetNewsType() 
        {
            var staff = new List<NewsType>{
                 new NewsType{ Id = 1, NewsCategory ="Sports"},
                 new NewsType{ Id = 2,  NewsCategory ="Political"}
            };
            Staff = new SelectList(staff, nameof(NewsType.Id), nameof(NewsType.NewsCategory));
        }

        public void GetMessageBroker()
        {
            var staff = new List<NewsType>{
                 new NewsType{ Id = 1, NewsCategory ="Redis"},
                 new NewsType{ Id = 2,  NewsCategory ="RabittMQ"}
            };
            MB = new SelectList(staff, nameof(NewsType.Id), nameof(NewsType.NewsCategory));
        }

    }

    public class NewsType
    {
        public int Id { get; set; }
        public string NewsCategory { get; set; }

    }
}
