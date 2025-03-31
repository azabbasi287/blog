using blog_servises.DTOs.Users;
using blog_servises.Servises.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace blog.Pages.Auth
{
    [BindProperties]
    [ValidateAntiForgeryToken]
    public class LoginModel : PageModel
    {
        private readonly Iusers _Iuser;
        public LoginModel(Iusers userService)
        {
            _Iuser = userService;
        }

        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        public string username { get; set; }

        public string password { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            var user = _Iuser.LoginUser(new UserLoginDto()
            {
                Password = password,
                UserName = username
            });

            if (user == null)
            {
                ModelState.AddModelError("UserName", "کاربری با مشخصات وارد شده یافت نشد");
                return Page();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim("Test","Test"),
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.FullName),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            HttpContext.SignInAsync(claimPrincipal, properties);
            return RedirectToPage("../Index");
        }
    }
}
