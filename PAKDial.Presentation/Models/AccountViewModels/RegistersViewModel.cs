using System.ComponentModel.DataAnnotations;

namespace PAKDial.Presentation.Models.AccountViewModels
{
    public class RegistersViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        //[Display(Name = "LastName")]
        //public string LastName { get; set; }
        [Required]
        [Phone]
        [StringLength(11, ErrorMessage = "Must be 11 Digits.", MinimumLength = 11)]
        [Display(Name = "MobileNo")]
        public string MobileNo { get; set; }
        //[Required]
        //[RegularExpression("^[0-9]{5}-[0-9]{7}-[0-9]$", ErrorMessage = "CNIC No must follow the XXXXX-XXXXXXX-X format!")]
        //[StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 15)]
        //public string Cnic { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }

        //[Range(typeof(bool), "true", "true", ErrorMessage = "You Must Check the Term&Conditions!")]
        //public bool TermsAndConditions { get; set; }

    }
}
