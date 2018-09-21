using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ContactManagementApp.Models
{
    public class ContactViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ContactViewModel()
        {
        }

        public List<SelectListItem> ListItems { get; set; }
        public int SelectedItem { get; set; }
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = " First name is mandatory")]
        [MaxLength(40, ErrorMessage = "Field cannot be longer than 40 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = " Last name is mandatory")]
        [MaxLength(40, ErrorMessage = "Field cannot be longer than 40 characters.")]
        public string LastName { get; set; }

        [Display(Name = "Home Phone")]
        [Required(ErrorMessage = "You must provide a phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = " Email is Mandatory")]
        [MaxLength(40, ErrorMessage = "Field cannot be longer than 40 characters.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        public string Status { get; set; }
    }
}