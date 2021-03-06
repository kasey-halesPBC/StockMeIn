using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockMeIn.Models
{
    public class CustomerVehicle
    {  // Model for customer vehicle table
        [Display(Name = "Cust Veh ID")]
        public int ID { get; set; }
        [Required]
        public int CustID { get; set; }
        [Display(Name = "Stock#")]
        [Required]
        public int CustStockID { get; set; }
        // Date datatype
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [Required]
        public DateTime StartDate { get; set; }
        // Date datatype
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}
