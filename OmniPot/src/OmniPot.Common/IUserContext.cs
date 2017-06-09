using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace OmniPot.Common
{
    public interface IUserContext
    {
        ClaimsPrincipal CurrentUser { get; }
        Guid UserId { get; }
    }
}
