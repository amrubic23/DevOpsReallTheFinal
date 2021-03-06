﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication15.Data;

namespace WebApplication15.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication15.Data.WebApplication15Context _context;

        public IndexModel(WebApplication15.Data.WebApplication15Context context)
        {
            _context = context;
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.SetString("loggedin", "f");
            HttpContext.Session.SetString("username", "");
            return RedirectToPage("Login");
        }
        public IList<Request> Request { get;set; }

        public async Task OnGetAsync()
        {
            Request = await _context.Request.ToListAsync();
            if ((String)HttpContext.Session.GetString("username") != "admin")
                Response.Redirect("Request");

            HttpContext.Session.SetString("loggedin", "t");
            HttpContext.Session.SetString("username", (String)HttpContext.Session.GetString("username"));
        }

        public IActionResult OnPostUpdate()
        {
                return RedirectToPage("Update");
        }
    }
}
