using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authy.Net;
using Microsoft.Extensions.Options;

namespace OmniPot.Services
{
    public class AuthyService : IAuthyService
    {
        private readonly Authy.Net.AuthyClient client;
        private readonly AuthyOptions options; 
        public AuthyService(IOptions<AuthyOptions> options)
        {
            this.options = options.Value; 
            client = new AuthyClient(this.options.AuthyApiKey, this.options.IsTesting);
        }

        public RegisterUserResult RegisterUser(string email, string cellPhoneNumber, int countryCode = 1)
        {
            return client.RegisterUser(email, cellPhoneNumber, countryCode);
        }

        public SendSmsResult SendSms(string userId)
        {
            return SendSms(userId, options.ForceSms);
        }

        public SendSmsResult SendSms(string userId, bool force = false)
        {
            return client.SendSms(userId, force);
        }

        public VerifyTokenResult VerifyToken(string userId, string token)
        {
            return VerifyToken(userId, token, options.ForceVerfication);
        }

        public VerifyTokenResult VerifyToken(string userId, string token, bool force = false)
        {
            return client.VerifyToken(userId, token, force);
        }
    }
}
