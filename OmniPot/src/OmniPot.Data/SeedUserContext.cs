using OmniPot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace OmniPot.Data
{
    /// <summary>
    /// This class is only used when seeding data. It is also required for dnx for migrations so that it knows where to find an instance of IUserContext.
    /// </summary>
    public class SeedUserContext : IUserContext
    {
        public ClaimsPrincipal CurrentUser
        {
            get
            {
                throw new NotImplementedException("GetCurrentUser not implemented");
            }
        }

        public Guid UserId
        {
            get
            {
                return Guid.Parse("5EED0000-0000-0000-0000-000000000000");
            }
        }
    }
}
