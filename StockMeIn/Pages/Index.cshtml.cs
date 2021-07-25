using StockMeIn.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StockMeIn.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;
        public IndexModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // User properties
        [BindProperty]
        [Required]
        [Display(Name = "User Name:")]
        public string UserName { get; set; }
        [BindProperty, DataType(DataType.Password)]
        [Required]
        [Display(Name = "Password:")]
        public string Password { get; set; }
        public string Message { get; set; }
        public async Task<IActionResult> OnPost()
        {
            // Get user info from appsettings.json file
            var user = configuration.GetSection("SiteUser").Get<SiteUser>();

            if (UserName == user.UserName)
            {  // if user name is found set password hasher variable
                var passwordHasher = new PasswordHasher<string>();
                if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
                {  // User password hasher to decrypt password and veriry. If verified set claim type and identity
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, UserName)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    // Direct to the vehicles page
                    return RedirectToPage("/vehicles/index");
                }  // End if
            }  // End if
            // If not verified display invalid and return to login page
            Message = "Invalid Username and Password";
            return Page();
        }
    }
}
