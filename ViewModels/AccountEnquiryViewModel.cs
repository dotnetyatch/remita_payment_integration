using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaMiddleWare.ViewModels
{
    public class AccountEnquiryRequest
    {
        public string accountNo { get; set; }
        public string bankCode { get; set; }
    }

    public class AccountEnquiryData
    {
        public string responseId { get; set; }
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
        public string accountName { get; set; }
        public string accountNo { get; set; }
        public string bankCode { get; set; }
        public object phoneNumber { get; set; }
        public object email { get; set; }
    }

    public class AccountEnquiryResponse
    {
        public string status { get; set; }
        public AccountEnquiryData data { get; set; }
    }
}
