using ProNotes.AppLib.Tools;
using System.ComponentModel.DataAnnotations;

namespace ProNotes.AppLib.MVC.ViewModels
{
    public class LoginViewModel
    {
        // {0} display name of property,
        // {1} is the MaximumLength,
        // {2} is the MinimumLength
        //[Required(ErrorMessage = "{0} is required")]
        [Required]
        [StringLength(255, ErrorMessage = "Must {0} must be between {2} and {1} characters", MinimumLength = 5)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }

        public CaptchaResult Captcha { get; set; } = new CaptchaResult();
    }
}
