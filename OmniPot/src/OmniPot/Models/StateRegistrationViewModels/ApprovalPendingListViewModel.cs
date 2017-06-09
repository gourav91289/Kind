using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OmniPot.Data.Models;

namespace OmniPot.Models.StateRegistrationViewModels
{
    /// <summary>
    /// For use with Pending Approvals Admin Screen
    /// </summary>
    public class ApprovalPendingListViewModel
    {
        public class ApprovalListViewModel
        {
            public Guid OrderId { get; set; }
            public string DaysSinceOrder { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string LicenseNumber { get; set; }
            public string OrderQty { get; set; }
            public string OrderDate { get; set; }
            public int PersonType { get; set; }
        }
    }
}