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
        [Required]
        public string VIN { get; set; }
        [Display(Name = "Type")]
        public char typeNU { get; set; }
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

        [DataType(DataType.Date)]
        [Display(Name = "In Inventory")]
        [Required]
        public DateTime inventoryDate { get; set; }
        [Display(Name = "Cost")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal cost { get; set; }
        [Display(Name = "Price")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal salePrice { get; set; }
        [Display(Name = "Status")]
        public char status { get; set; }
    }
}
