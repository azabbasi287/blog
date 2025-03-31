using blog_servises.DTOs.Users;
using blog_servises.Servises.Users;
using CodeYad_Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace blog.Pages.Auth
{
    [BindProperties]
    [ValidateAntiForgeryToken]
    public class registerModel : PageModel
    {
        private readonly Iusers _Iuser;
        #region properties
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string FullName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(6, ErrorMessage = "{0} باید بیشتر از 5 کاراکتر باشد")]
        public string Password { get; set; }
        
        #endregion
        public registerModel(Iusers userService)
        {
            _Iuser = userService;
        }

        public IActionResult OnPost()
        {
            var result = _Iuser.RegisterUser(new UserRegisterDto()
            {
                UserName = UserName,
                Password = Password,
                FullName = FullName
            });
            if (result.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError("UserName", result.Message);
                return Page();
            }
            return RedirectToPage("Login");

        }

        public void OnGet()
        {
        }
        
    }
}
