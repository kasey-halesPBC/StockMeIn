using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMeIn.Models
{
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
        public string State { get; set; }
        public string Zip { get; set; }
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        public string HomePhone { get; set; }
        [Display(Name = "Work Phone")]
        [DataType(DataType.PhoneNumber)]
        public string WorkPhone { get; set; }
        [Display(Name = "Cell Phone")]
        [DataType(DataType.PhoneNumber)]
        public string CellPhone { get; set; }
        public string Notes { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Created")]
        [Required]
        public DateTime CreateDAte { get; set; }
    }
}
