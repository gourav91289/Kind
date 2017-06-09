using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class StateLicense
    {
        public Guid StateLicenseId { get; set; }
        public string LicenseNumber { get; set; }
        //The two columns below aren't used currently but I thought I'd import them anyway where they to change their minds
        public bool IsDisabledVeteran { get; set; }
        public bool IsSsi { get; set; }
        public bool IsMedicaid { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime Expiry { get; set; }
        public bool IsHospice { get; set; }
        public StateLicenseStatus Status { get; set; }

        public string CaretakerLicenseNumber { get; set; }
        public DateTime CaretakerIssueDate { get; set; }
        public DateTime CaretakerExpiry { get; set; }
        public bool CaretakerIsMedicaid { get; set; }
    }
}
