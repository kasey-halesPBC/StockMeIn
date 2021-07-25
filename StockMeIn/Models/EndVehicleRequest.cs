using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMeIn.Models
{
    public class EndVehicleRequest
    {   // Model to end vehicle ownership on customer information page
        public int CustVehicleID { get; set; }
        public int CustID { get; set; }
    }
}
