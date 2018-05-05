using System.ComponentModel.DataAnnotations;

namespace $safeprojectname$.ViewModels
{
    public class TokenViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required, MinLength(6), MaxLength(32)]
        public string Password { get; set; }
    }
}