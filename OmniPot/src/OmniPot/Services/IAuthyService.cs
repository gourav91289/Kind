using Authy.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Services
{
    public interface IAuthyService
    {
        RegisterUserResult RegisterUser(string email, string cellPhoneNumber, int countryCode = 1);
        VerifyTokenResult VerifyToken(string userId, string token, bool force = false);
        SendSmsResult SendSms(string userId, bool force = false);
    }
}
