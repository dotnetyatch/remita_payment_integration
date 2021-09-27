using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaMiddleWare.ViewModels
{
    public class SinglePaymentRequest
    {
            public string toBank { get; set; }
            public string creditAccount { get; set; }
            public string narration { get; set; }
            public string amount { get; set; }
            public string transRef { get; set; }
            public string fromBank { get; set; }
            public string debitAccount { get; set; } // account number to be debited
            public string beneficiaryEmail { get; set; }
    }

    public class EncryptSinglePaymentRequest
    {
        public string toBank { get; set; }
        public string creditAccount { get; set; }
        public string narration { get; set; }
        public string amount { get; set; }
        public string transRef { get; set; }
        public string fromBank { get; set; }
        public string debitAccount { get; set; } // account number to be debited
        public string beneficiaryEmail { get; set; }
    }

    public class SinglePaymentResponse
    {
        public string status { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string authorizationId { get; set; }
        public string transRef { get; set; }
        public string transDate { get; set; }
        public string paymentDate { get; set; }
        public string responseId { get; set; }
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
        public string rrr { get; set; }
        public object data { get; set; }
    }

    public class NAVSinglePaymentResponse
    {
        public string transRef { get; set; }
        public string rrr { get; set; }
    }

    public class NAVSinglePaymentRequest
    {
        public string toBank { get; set; }
        public string creditAccount { get; set; }
        public string narration { get; set; }
        public string amount { get; set; }
        public string transRef { get; set; }
        public string beneficiaryEmail { get; set; }
    }


}
