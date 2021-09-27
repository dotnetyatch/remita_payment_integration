using RemitaMiddleWare.Helpers;
using RemitaMiddleWare.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RemitaMiddleWare.Services
{
    public class IntegrationService
    {
        Utility util = new Utility();
        string baseUrl = Startup.ConfigurationRoot["RemittaConfig:BaseUrl"];
        string environment = Startup.ConfigurationRoot["RemittaConfig:Environment"];
        string merchantid;
        string apikey;
        string apitoken;
        string iv;
        string enckey;

            public IntegrationService(string _merchantid, string _apikey, string _apitoken, string _iv, string _enckey)
        {
            merchantid = _merchantid;
            apikey = _apikey;
            apitoken = _apitoken;
            iv = _iv;
            enckey = _enckey;

        }


        public async Task<SinglePaymentResponse> SinglePay(SinglePaymentRequest navrequest)
        {
            CTrace ct = new CTrace();
            var resp = new SinglePaymentResponse();
            string requestId = Utility.Generate(12);
            try
            {
                string requestts = DateTime.Now.ToString();
                string apidetailshash = "";
                Encryption enUtil = new Encryption();
                EncryptSinglePaymentRequest encryptSinglePayment = new EncryptSinglePaymentRequest();
                encryptSinglePayment.amount = enUtil.Encrypt(navrequest.amount, enckey, iv);
                encryptSinglePayment.debitAccount = enUtil.Encrypt(navrequest.debitAccount, enckey, iv);
                encryptSinglePayment.creditAccount = enUtil.Encrypt(navrequest.creditAccount, enckey, iv);
                encryptSinglePayment.fromBank = enUtil.Encrypt(navrequest.fromBank, enckey, iv);
                encryptSinglePayment.creditAccount = enUtil.Encrypt(navrequest.creditAccount, enckey, iv);
                encryptSinglePayment.toBank = enUtil.Encrypt(navrequest.toBank, enckey, iv);
                encryptSinglePayment.transRef = enUtil.Encrypt(navrequest.transRef, enckey, iv);
                encryptSinglePayment.narration = enUtil.Encrypt(navrequest.narration, enckey, iv);
                string hash_string = apikey + requestId + apitoken;
                string hashed = Encryption.SHA512(hash_string);
                Dictionary<string, string> header = new Dictionary<string, string>() { { "MERCHANT_ID", merchantid }, { "API_KEY", apikey }, { "REQUEST_ID", requestId }, { "REQUEST_TS", Utility.getTimeStamp() }, { "API_DETAILS_HASH", hashed } };
                if (environment == "test")
                {
                    string body = JsonConvert.SerializeObject(navrequest);
                    string head = JsonConvert.SerializeObject(header);
                    ct.argsLogger("Header: " + head + "Body " + " " + body);
                }
                HttpResponseMessage httpResponse = await util.MakeHttpRequest(encryptSinglePayment, baseUrl, "rpg/api/v2/merc/payment/singlePayment.json", HttpMethod.Post, header);
                if (httpResponse != null && httpResponse.IsSuccessStatusCode)
                {
                    string stringContent = await httpResponse.Content.ReadAsStringAsync();
                    resp = JsonConvert.DeserializeObject<SinglePaymentResponse>(stringContent);

                }
            }
            catch(Exception ex)
            {
                resp.status = "fail";
                resp.data.responseDescription = ex.ToString();
                ct.logger(requestId + " " + ex.ToString());
            }
            return resp;

        }

        public async Task<BulkPaymentResponse> BulkPay(BulkPaymentRequest navrequest)
        {
            CTrace ct = new CTrace();
            var resp = new BulkPaymentResponse();
            string requestId ="";
            try
            {

                requestId = Utility.Generate(12);
                string requestts = "";
                string apidetailshash = "";
                Encryption enUtil = new Encryption();
                Dictionary<string, Object> paymentData = new Dictionary<string, Object>();
                List<PaymentDetail> paymentDetailListFrmBulkPayload = navrequest.paymentDetails;
                List<EncryptPaymentDetails> encryptPaymentListFrmBulkPayloadList = new List<EncryptPaymentDetails>();
                //double sumPaymentDetailAmount = 0;
                EncryptPaymentDetails encryptPaymentListFrmBulkPayload = null;
                foreach (PaymentDetail pDetails in paymentDetailListFrmBulkPayload)
                {
                    encryptPaymentListFrmBulkPayload = new EncryptPaymentDetails();
                    encryptPaymentListFrmBulkPayload.transRef = enUtil.Encrypt(pDetails.transRef, enckey, iv);
                    encryptPaymentListFrmBulkPayload.narration = enUtil.Encrypt(pDetails.narration, enckey, iv);
                    encryptPaymentListFrmBulkPayload.benficiaryEmail = enUtil.Encrypt(pDetails.benficiaryEmail, enckey, iv);
                    encryptPaymentListFrmBulkPayload.benficiaryBankCode = enUtil.Encrypt(pDetails.benficiaryBankCode, enckey, iv);
                    encryptPaymentListFrmBulkPayload.benficiaryAccountNumber = enUtil.Encrypt(pDetails.benficiaryAccountNumber, enckey, iv);
                    encryptPaymentListFrmBulkPayload.amount = enUtil.Encrypt(pDetails.amount.ToString(), enckey, iv);
                    encryptPaymentListFrmBulkPayload.benficiaryName = enUtil.Encrypt(pDetails.benficiaryName.ToString(), enckey, iv);
                    // sumPaymentDetailAmount += pDetails.amount;
                    encryptPaymentListFrmBulkPayloadList.Add(encryptPaymentListFrmBulkPayload);
                }
                BulkPaymentInfo bulkPaymentInfoFrmBulkPaymentPayload = navrequest.bulkPaymentInfo;
                var batchRef = enUtil.Encrypt(bulkPaymentInfoFrmBulkPaymentPayload.batchRef, enckey, iv);
                var debitAccount = enUtil.Encrypt(bulkPaymentInfoFrmBulkPaymentPayload.debitAccount, enckey, iv);
                var narration = enUtil.Encrypt(bulkPaymentInfoFrmBulkPaymentPayload.narration, enckey, iv);
                var bankCode = enUtil.Encrypt(bulkPaymentInfoFrmBulkPaymentPayload.bankCode, enckey, iv);
                var totalAmount = enUtil.Encrypt(bulkPaymentInfoFrmBulkPaymentPayload.totalAmount, enckey, iv);
                EncryptBulkPaymentInfo encryptedBulkPaymentInfoFrmBulkPaymentPayload = new EncryptBulkPaymentInfo();
                encryptedBulkPaymentInfoFrmBulkPaymentPayload.batchRef = batchRef;
                encryptedBulkPaymentInfoFrmBulkPaymentPayload.debitAccount = debitAccount;
                encryptedBulkPaymentInfoFrmBulkPaymentPayload.narration = narration;
                encryptedBulkPaymentInfoFrmBulkPaymentPayload.bankCode = bankCode;
                encryptedBulkPaymentInfoFrmBulkPaymentPayload.totalAmount = totalAmount;
                string hash_string = apikey + requestId + apitoken;
                string hashed = Encryption.SHA512(hash_string);
                Dictionary<string, string> header = new Dictionary<string, string>() { { "MERCHANT_ID", merchantid }, { "API_KEY", apikey }, { "REQUEST_ID", requestId }, { "REQUEST_TS", Utility.getTimeStamp() }, { "API_DETAILS_HASH", hashed } };

                EncryptedBulkPaymentRequest encryptedrequest = new EncryptedBulkPaymentRequest();
                encryptedrequest.bulkPaymentInfo = encryptedBulkPaymentInfoFrmBulkPaymentPayload;
                encryptedrequest.paymentDetails = encryptPaymentListFrmBulkPayloadList;
                if (environment == "test")
                {
                    string body = JsonConvert.SerializeObject(navrequest);
                    string head = JsonConvert.SerializeObject(header);
                    string encryptedbody = JsonConvert.SerializeObject(encryptedrequest);
                    ct.argsLogger("Header: " + head + "Body " + " " + body + " " +  "encrypted : " + encryptedbody);
                }
               
                HttpResponseMessage httpResponse = await util.MakeHttpRequest(encryptedrequest, baseUrl, "rpg/api/v2/merc/bulk/payment/send", HttpMethod.Post, header);
                if (httpResponse != null && httpResponse.IsSuccessStatusCode)
                {
                    string stringContent = await httpResponse.Content.ReadAsStringAsync();
                    resp = JsonConvert.DeserializeObject<BulkPaymentResponse>(stringContent);

                }
            }
            catch(Exception ex)
            {
                resp.status = "fail";
                resp.data.responseDescription = ex.ToString();
                ct.logger(requestId + " " + ex.ToString());
            }
            return resp;

        }

        public async Task<BulkPaymentStatusResponse> BulkPayStatus(BulkPaymentStatusRequest navrequest)
        {
            var resp = new BulkPaymentStatusResponse();
            string requestId = Utility.Generate(12);
            Encryption enUtil = new Encryption();
            navrequest.batchRef = enUtil.Encrypt(navrequest.batchRef, enckey, iv);
            string hash_string = apikey + requestId + apitoken;
            string hashed = Encryption.SHA512(hash_string);
            Dictionary<string, string> header = new Dictionary<string, string>() { { "MERCHANT_ID", merchantid }, { "API_KEY", apikey }, { "REQUEST_ID", requestId }, { "REQUEST_TS", Utility.getTimeStamp() }, { "API_DETAILS_HASH", hashed } };
            HttpResponseMessage httpResponse = await util.MakeHttpRequest(navrequest, baseUrl, "rpg/api/v2/merc/bulk/payment/status", HttpMethod.Post, header);
            if (httpResponse != null && httpResponse.IsSuccessStatusCode)
            {
                string stringContent = await httpResponse.Content.ReadAsStringAsync();
                resp = JsonConvert.DeserializeObject<BulkPaymentStatusResponse>(stringContent);

            }
            return resp;
        }

        public async Task<SinglePaymentStatusResponse> SinglePayStatus(SinglePaymentStatusRequest navrequest)
        {
            var resp = new SinglePaymentStatusResponse();
            Encryption enUtil = new Encryption();
            string requestId = Utility.Generate(12);
            string requestts = "";
            string apidetailshash = "";
            navrequest.transRef = enUtil.Encrypt(navrequest.transRef, enckey, iv);
            string hash_string = apikey + requestId + apitoken;
            string hashed = Encryption.SHA512(hash_string);
            Dictionary<string, string> header = new Dictionary<string, string>() { { "MERCHANT_ID", merchantid }, { "API_KEY", apikey }, { "REQUEST_ID", requestId }, { "REQUEST_TS", Utility.getTimeStamp() }, { "API_DETAILS_HASH", hashed } };
            HttpResponseMessage httpResponse = await util.MakeHttpRequest(navrequest, baseUrl, "rpg/api/v2/merc/payment/status", HttpMethod.Post, header);
            if (httpResponse != null && httpResponse.IsSuccessStatusCode)
            {
                string stringContent = await httpResponse.Content.ReadAsStringAsync();
                resp = JsonConvert.DeserializeObject<SinglePaymentStatusResponse>(stringContent);

            }
            return resp;
        }

        public async Task<AccountEnquiryResponse> AccountEnquiry(AccountEnquiryRequest navrequest)
        {
            var resp = new AccountEnquiryResponse();
            CTrace ct = new CTrace();
            string requestId = Utility.Generate(12);
            try
            {

                
                Encryption enUtil = new Encryption();
                navrequest.accountNo = enUtil.Encrypt(navrequest.accountNo, enckey, iv);
                navrequest.bankCode = enUtil.Encrypt(navrequest.bankCode, enckey, iv);
                string hash_string = apikey + requestId + apitoken;
                string hashed = Encryption.SHA512(hash_string);
                Dictionary<string, string> header = new Dictionary<string, string>() { { "MERCHANT_ID", merchantid }, { "API_KEY", apikey }, { "REQUEST_ID", requestId }, { "REQUEST_TS", Utility.getTimeStamp() }, { "API_DETAILS_HASH", hashed } };
                HttpResponseMessage httpResponse = await util.MakeHttpRequest(navrequest, baseUrl, "rpg/api/v2/merc/fi/account/lookup", HttpMethod.Post, header);
                if (httpResponse != null && httpResponse.IsSuccessStatusCode)
                {
                    string stringContent = await httpResponse.Content.ReadAsStringAsync();
                    resp = JsonConvert.DeserializeObject<AccountEnquiryResponse>(stringContent);

                }
                if (environment == "test")
                {
                    string body = JsonConvert.SerializeObject(navrequest);
                    string head = JsonConvert.SerializeObject(header);
                    ct.argsLogger("RequestID" + " " + requestId + "Header: " + head + "Encrypted Body " + " " + body);
                }
            }
            catch(Exception ex)
            {
                resp.status = "false";
                resp.data.responseDescription = "Handled Exception:" + ex.ToString();
                ct.logger(requestId + " " + ex.ToString());
            }
            return resp;
        }

    }

}
