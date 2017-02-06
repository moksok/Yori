using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class RegistrationRequest
    {

        //server side validation - check for proper email format

        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Proper Email Format Required")]
        public string Email { get; set; }

        //server side validation
        //Regex command checks for in the following order
        // - At least one lowercase character
        // - At least one uppercase character
        // - At least one decimal character
        // - At least one special character(non number or letter)
        // - Minimum of 6 characters
        // - NOTE: This regex expression allows for white space

        [Required(ErrorMessage = "Password Required")]

        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Username Required")]
        //[RegularExpression(@"^[^0-9\\s]+$", ErrorMessage = "Username does not meet minimum requirements")]
        public string Username { get; set; } = null;

        public string FirstName { get; set; } = null;

        public string LastName { get; set; } = null;
    }
}