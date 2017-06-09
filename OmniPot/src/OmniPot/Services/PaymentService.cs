using Common.Payment.Common1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace OmniPot.Services
{
    public class PaymentService : IPaymentService
    {
#if DEBUG
        private const string MerchantCode = "DBR_KIND";
        private const string MerchantKey = "ASDFASDFasdfasdf12341234!12345!";
        private const string ServiceCode = "KIND_PAYMENTS";
        private const string ServiceEndpoint = "https://stageccp.dev.cdc.nicusa.com/CommonCheckout/CCPWebService/ServiceWeb.svc";
        private const string RedirectUrlFormat = "https://stageccp.dev.cdc.nicusa.com/CommonCheckout/Commoncheckpage/Default.aspx?token={0}";
#else
        private const string MerchantCode = "RIKIND";        
        private const string MerchantKey = "B05baf589f8#e721fA38636819466e00";
        private const string ServiceCode = "KIND_PAYMENTS";
        private const string ServiceEndpoint = "https://securecheckout.cdc.nicusa.com/ccpwebservice/ServiceWeb.svc";
        private const string RedirectUrlFormat = "https://securecheckout.cdc.nicusa.com/CommonCheckPage/Default.aspx?token={0}";
#endif
        private const string StateCode = "RI";

        public PreparePaymentResult PreparePayment(PreparePaymentRequest request)
        {
            var proxy = CreateProxy();
            var paymentRequest = new PaymentInfov2
            {
                ADDRESS1 = request.Address1,
                ADDRESS2 = request.Address2,
                //CID = request.CustomerId,
                CITY = request.City,
                COMPANYNAME = request.CompanyName,
                AMOUNT = request.Amount,
                COUNTRY = request.Country,
                DESCRIPTION = request.Description,
                EMAIL = request.Email,
                MERCHANTID = request.MerchantId ?? MerchantCode,
                MERCHANTKEY = request.MerchantKey ?? MerchantKey,
                NAME = request.Name,
                STATE = request.State,
                ZIP = request.Zip,
                PHONE = request.Phone,
                UNIQUETRANSID = Guid.NewGuid().ToString(),
                HREFCANCEL = request.HrefCancel,
                HREFDUPLICATE = request.HrefDuplicate,
                HREFFAILURE = request.HrefFailure,
                HREFSUCCESS = request.HrefSuccess,
                STATECD = request.StateCode ?? StateCode,
                SERVICECODE = request.ServiceCode ?? ServiceCode,
                LOCALREFID = request.PlantTagOrderId.ToString().Replace("-", string.Empty)
            };

            var items = new List<LINEITEM>();
            
            foreach (var item in request.OrderItems)
                items.Add(new LINEITEM { DESCRIPTION = item.Description, ITEM_ID = item.ItemId, QUANTITY = item.Quantity, SKU = item.Sku, UNIT_PRICE = item.UnitPrice });
            //2.5m line item for us. 
//#if !DEBUG
            items.Add(new LINEITEM { DESCRIPTION = "Processing Fee", ITEM_ID = int.MaxValue, QUANTITY = 1, SKU = "2020H", UNIT_PRICE = 2.50m });
//#endif

            paymentRequest.LINEITEMS = new LINEITEM[items.Count];
            items.CopyTo(paymentRequest.LINEITEMS);            

            var result = proxy.PreparePaymentv2(paymentRequest);

            var redirectUrl = result.ERRORCODE == 0 ? string.Format(RedirectUrlFormat, result.TOKEN) : string.Empty; 

            return new PreparePaymentResult { ErrorCode = result.ERRORCODE, ErrorMessage = result.ERRORMESSAGE, Token = result.TOKEN, RedirectUrl = redirectUrl };
        }

        private IServiceWeb CreateProxy()
        {
            ChannelFactory<IServiceWeb> factory = null;
            IServiceWeb serviceProxy = null;
            Binding binding = null;

            binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            factory = new ChannelFactory<IServiceWeb>(binding, new EndpointAddress(ServiceEndpoint));
            serviceProxy = factory.CreateChannel();

            return serviceProxy;
        }
    }
}
