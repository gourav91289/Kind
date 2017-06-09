using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Services
{
    public class PreparePaymentRequest
    {
        public Guid PlantTagOrderId { get; set; }
        public string StateCode { get; set; }
        public string Amount { get; set; }
        public string CustomerId { get; set; }
        public string UniqueTransactionId { get; set; }
        public string Description { get; set; }
        public string MerchantId { get; set; }
        public string MerchantKey { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string HrefSuccess { get; set; }
        public string HrefFailure { get; set; }
        public string HrefDuplicate { get; set; }
        public string HrefCancel { get; set; }
        public string ServiceCode { get; set; }
        public List<PreparePaymentRequestItem> OrderItems { get; set; } = new List<PreparePaymentRequestItem>();

    }
    public class PreparePaymentRequestItem
    {
        public int ItemId { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
    }
    public class PreparePaymentResult
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public string RedirectUrl { get; set; }
    }

    public class OrderInfoResponse
    {
        public Guid PlantTagOrderId { get; set; }
        public string StateCode { get; set; }
        public string Amount { get; set; }
        public string CustomerId { get; set; }
        public string UniqueTransactionId { get; set; }
        public string Description { get; set; }
        public string MerchantId { get; set; }
        public string MerchantKey { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string ServiceCode { get; set; }


        ////
    }
    public interface IPaymentService
    {
        PreparePaymentResult PreparePayment(PreparePaymentRequest request);
    }
}
