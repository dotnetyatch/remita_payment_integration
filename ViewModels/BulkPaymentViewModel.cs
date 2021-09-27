using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaMiddleWare.ViewModels
{
    public class BulkPaymentInfo
    {
        public string totalAmount { get; set; }
        public string batchRef { get; set; }
        public string debitAccount { get; set; }
        public string narration { get; set; }
        public string bankCode { get; set; }
    }

    public class PaymentDetail
    {
        public string amount { get; set; }
        public string benficiaryAccountNumber { get; set; }
        public string benficiaryBankCode { get; set; }
        public string benficiaryEmail { get; set; }
        public string benficiaryLocation { get; set; }
        public string benficiaryName { get; set; }
        public string benficiaryPhone { get; set; }
        public string currencyCode { get; set; }
        public string narration { get; set; }
        public string originalAccountNumber { get; set; }
        public string paymentReference { get; set; }
        public string transRef { get; set; }
    }

    public class BulkPaymentRequest
    {
        public BulkPaymentInfo bulkPaymentInfo { get; set; }
        public List<PaymentDetail> paymentDetails { get; set; }
    }

    public class BulkPaymentResponse
    {
        public string status { get; set; }
        public BulkData data { get; set; }
    }

    public class BulkData
    {
        public string authorizationId { get; set; }
        public string transRef { get; set; }
        public string transDate { get; set; }
        public string paymentDate { get; set; }
        public string responseId { get; set; }
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
        public string rrr { get; set; }
    }

    public class NAVBulkPaymentInfo
    {
        public string totalAmount { get; set; }
        public string batchRef { get; set; }
        public string debitAccount { get; set; }
        public string narration { get; set; }
        public string bankCode { get; set; }
    }

    public class NAVPaymentDetail
    {
        public string amount { get; set; }
        public string benficiaryAccountNumber { get; set; }
        public string benficiaryBankCode { get; set; }
        public string benficiaryEmail { get; set; }
        public string benficiaryLocation { get; set; }
        public string benficiaryName { get; set; }
        public string benficiaryPhone { get; set; }
        public string currencyCode { get; set; }
        public string narration { get; set; }
        public string originalAccountNumber { get; set; }
        public string paymentReference { get; set; }
        public string transRef { get; set; }
    }

    public class NAVBulkPaymentRequest
    {
        public NAVBulkPaymentInfo bulkPaymentInfo { get; set; }
        public List<NAVPaymentDetail> paymentDetails { get; set; }
    }

    public class NAVBulkPaymentResponse
    {
        public string transRef { get; set; }
        public string rrr { get; set; }
    }

    public class EncryptBulkPaymentInfo
    {
        public string totalAmount { get; set; }

        public string paymentDetailsCount { get; set; }

        public string batchRef { get; set; }

        public long tsaServiceId { get; set; }

        public string debitAccount { get; set; }

        public string narration { get; set; }

        public string bankCode { get; set; }

        public string requestId { get; set; }
    }

   public class EncryptPaymentDetails
    {
        public string transRef { get; set; }

        public string paymentReference { get; set; }

        public string narration { get; set; }

        public string benficiaryName { get; set; }

        public string benficiaryEmail { get; set; }

        public string benficiaryPhone { get; set; }

        public string benficiaryLocation { get; set; }

        public string benficiaryBankCode { get; set; }

        public string benficiaryAccountNumber { get; set; }

        public string benficiaryBvn { get; set; }

        public string amount { get; set; }

        public string currencyCode { get; set; }

        public string originalName { get; set; }

        public string originalEmail { get; set; }

        public string originalPhone { get; set; }

        public string originalLocation { get; set; }

        public string originalBankCode { get; set; }

        public string originalAccountNumber { get; set; }

        public string rrrNumber { get; set; }
    }

    public class EncryptedBulkPaymentRequest
    {
        public EncryptBulkPaymentInfo bulkPaymentInfo { get; set; }
        public List<EncryptPaymentDetails> paymentDetails { get; set; }
    }
}
