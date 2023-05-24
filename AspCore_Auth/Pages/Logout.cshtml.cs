using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspCore_Auth.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signManager;

        public LogoutModel (SignInManager<IdentityUser> signManager)
        {
            this.signManager = signManager;
        }
        public void OnGet()
        {
        }   
        public async Task<IActionResult> OnPostAsync()
        {
            await signManager.SignOutAsync();
            return RedirectToPage("LogIn");
        }
        public async Task<IActionResult> OnPostDontLogoutAsync()
        {
            return RedirectToPage("Index");
        }

    }
}
