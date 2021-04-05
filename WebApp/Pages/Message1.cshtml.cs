using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class Message1Model : PageModel
    {
        public void OnGet(string msg)
        {
            ViewData["msg"] = msg;
        }

        public async Task<IActionResult> OnPostAsync()
        {   
            return RedirectToPage("PubSub");
        }
    }
}
