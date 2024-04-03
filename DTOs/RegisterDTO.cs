using System.ComponentModel.DataAnnotations;

namespace API_FinalTask.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "You must enter your name")]
        [MaxLength(20, ErrorMessage = " name must be less than 20 characters"), MinLength(3, ErrorMessage = " name must be more than 3 characters")]
        public string UserName { get; set; }

        [EmailAddress]
        public string Emil { get; set; }

        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}