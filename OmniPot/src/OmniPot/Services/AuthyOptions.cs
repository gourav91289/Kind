using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Services
{
    public class AuthyOptions
    {
        /// <summary>
        /// The authy api key. This is stored in the secrets store.
        /// </summary>
        public string AuthyApiKey { get; set; }
        /// <summary>
        /// Uses the authy testing environment.
        /// </summary>
        public bool IsTesting { get; set; } = false;
        /// <summary>
        /// Forces the send of an sms even if the user has authy installed on their device. 
        /// </summary>
        public bool ForceSms { get; set; } = false;
        /// <summary>
        /// Determines if authy will verify even if the user hasn't completed registration.
        /// </summary>
        public bool ForceVerfication { get; set; } = false;
    }
}
