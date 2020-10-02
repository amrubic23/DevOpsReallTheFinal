using System;
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
    public class DetailsModel : PageModel
    {
        private readonly WebApplication15.Data.WebApplication15Context _context;

        public DetailsModel(WebApplication15.Data.WebApplication15Context context)
        {
            _context = context;
        }

        public Request Request { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if ((String)HttpContext.Session.GetString("username") != "admin")
                Response.Redirect("Request");
            HttpContext.Session.SetString("loggedin", "t");
            HttpContext.Session.SetString("username", (String)HttpContext.Session.GetString("username"));
            if (id == null)
            {
                return NotFound();
            }

            Request = await _context.Request.FirstOrDefaultAsync(m => m.username == id);

            if (Request == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
