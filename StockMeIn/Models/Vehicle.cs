using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMeIn.Models
{
    public class Vehicle
    {
        [Display(Name = "Stock#")]
        public int ID { get; set; }
        // Validate VIN is max length 17 and contains capital letters and numbers only
        [RegularExpression(@"[A-Z0-9]*$", ErrorMessage = "Must Be Capital Letters and Numbers Only")]
        [StringLength(17)]
        [Required]
        public string VIN { get; set; }
        [StringLength(1)]
        // Validate vehicle type is N or U
        [RegularExpression(@"[NU]*$", ErrorMessage = "Valid Entries: N for New, U for Used")]
        [Display(Name = "Type N/U")]
        [Required]
        public string typeNU { get; set; }
        [Display(Name = "Year")]
        [Required]
        public int year { get; set; }
        [Display(Name = "Make")]
        [Required]
        public string make { get; set; }
        [Display(Name = "Model")]
        [Required]
        public string model { get; set; }
        [Display(Name = "Body")]
        public string body { get; set; }
        [Display(Name = "Color")]
        public string color { get; set; }
        // Date datatype
        [DataType(DataType.Date)]
        [Display(Name = "In Inventory")]
        [Required]
        public DateTime inventoryDate { get; set; }

        [Display(Name = "Cost")]
        // 18 digit decimal with 2 decimal places
        [Column(TypeName = "decimal(18, 2)")]
        public decimal cost { get; set; }
        [Display(Name = "Price")]
        // 18 digit decimal with 2 decimal places
        [Column(TypeName = "decimal(18, 2)")]
        public decimal salePrice { get; set; }
        [Display(Name = "Status")]
        [StringLength(1)]
        // Validate status is C or I
        [RegularExpression(@"[CI]*$", ErrorMessage = "Valid Entries: I for Inventory, C for Customer")]
        public char status { get; set; }
    }
}
