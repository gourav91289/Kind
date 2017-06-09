using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Models.AccountViewModels
{
    public class SetRolesViewModel
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
