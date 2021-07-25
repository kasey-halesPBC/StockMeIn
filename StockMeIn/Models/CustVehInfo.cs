using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMeIn.Models
{
    public class CustVehInfo
    {  // Model to display customer vehicle info. Contains a vehicle object and a customer vehicle
       // to join the two and display proper vehicle information
        public Vehicle VehicleInfo { get; set; }
        public CustomerVehicle CustVehData { get; set; }
    }
}
