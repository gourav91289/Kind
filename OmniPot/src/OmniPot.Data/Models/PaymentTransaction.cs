using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Models
{
    public class PaymentTransaction : EntityBase 
    {
        public Guid PaymentTransactionId { get; set; }
        public Guid PlantTagOrderId { get; set; }
        public DateTime TransactionUtc { get; set; }
        public string CardType { get; set; }
        public string LastFour { get; set; }
        public string AvsResponse { get; set; }
        public string CvvResponse { get; set; }
        public decimal Amount { get; set; }
        public string OrderId { get; set; }
        public string AuthCode { get; set; }
        public string Token { get; set; }
    }
}
