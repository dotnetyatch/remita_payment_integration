using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaMiddleWare.ViewModels
{
    public class BulkPaymentStatusRequest
    {
        public string batchRef { get; set; }
    }

    public class BulkPaymentStatusInfo
    {
        public string debitAccountToken { get; set; }
        public double totalAmount { get; set; }
        public double feeAmount { get; set; }
        public string currencyCode { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string paymentState { get; set; }
    }

    public class ResponsePaymentDetail
    {
        public string transRef { get; set; }
        public string paymentReference { get; set; }
        public string authorizationId { get; set; }
        public string transDate { get; set; }
        public string paymentDate { get; set; }
        public double amount { get; set; }
        public string paymentState { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
    }

    public class ResponseData
    {
        public string bulkRef { get; set; }
        public string batchRef { get; set; }
        public BulkPaymentStatusInfo bulkPaymentStatusInfo { get; set; }
        public List<ResponsePaymentDetail> paymentDetails { get; set; }
    }

    public class BulkPaymentStatusResponse
    {
        public string status { get; set; }
        public ResponseData data { get; set; }
    }
}
