using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    [Flags]
    public enum ApplicationStatus
    {
        Approved = 1, 
        Denied = 2, 
        Pending = 4, 
        AwaitingPayment = 8, 
        //Awaiting shipment from kind to RI
        AwaitingShipment = 16, 
        AwaitingPickup = 32, 
        Complete = 64,
        Paid = 128, 
        Shipped = 254
    }
}
