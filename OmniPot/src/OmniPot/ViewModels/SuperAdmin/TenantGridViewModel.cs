using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.ViewModels.SuperAdmin
{
    public class TenantGridViewModel
    {
        public Guid TenantId { get; set; }
        public string DisplayName { get; set; }
        public string RouteName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
