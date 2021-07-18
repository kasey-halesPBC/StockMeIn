using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMeIn.Models
{  // Customer Model contains formatting and validation
    public class Customer
    {
        [Display(Name = "Cust ID")]
        public int ID { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Must Be Capitalized 2 Letter State Code")]
        [RegularExpression(@"[A-Z]*$", ErrorMessage = "Must Be Capitalized 2 Letter State Code")]
        public string State { get; set; }
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Must Be 5 Digit Numeric Zip Code")]
        [RegularExpression(@"[0-9]*$", ErrorMessage = "Must Be 5 Digit Numeric Zip Code")]
        public string Zip { get; set; }
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-]([0-9]{3})[-]([0-9]{4})$", 
            ErrorMessage = "Must be in Format ###-###-####")]
        public string HomePhone { get; set; }
        [Display(Name = "Work Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-]([0-9]{3})[-]([0-9]{4})$",
            ErrorMessage = "Must be in Format ###-###-####")]
        public string WorkPhone { get; set; }
        [Display(Name = "Cell Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-]([0-9]{3})[-]([0-9]{4})$",
            ErrorMessage = "Must be in Format ###-###-####")]
        public string CellPhone { get; set; }
        public string Notes { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Created")]
        [Required]
        public DateTime CreateDAte { get; set; }

        [RegularExpression
            (@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
