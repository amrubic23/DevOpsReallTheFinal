using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication15.Data;

namespace WebApplication15.Pages
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication15.Data.WebApplication15Context _context;

        public CreateModel(WebApplication15.Data.WebApplication15Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if ((String)HttpContext.Session.GetString("username") != "admin")
                Response.Redirect("Request");
            return Page();
        }

        [BindProperty]
        public Request Request { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Request.Add(Request);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
