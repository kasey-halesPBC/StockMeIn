using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMeIn.Models
{
    public class SellVehicleRequest
    {  // Model to sell vehicle on the sell vehicle page
        public int VehicleID { get; set; }
        public int CustomerID { get; set; }
    }
}
