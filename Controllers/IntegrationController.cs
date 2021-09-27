using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RemitaMiddleWare.Services;
using RemitaMiddleWare.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RemitaMiddleWare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationController : ControllerBase
    {

        [Route("SinglePayment")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SinglePaymentResponse))]
        public async Task<IActionResult> SinglePayment(SinglePaymentRequest request)
        {
          IntegrationService service = new IntegrationService(Request.Headers["MERCHANTID"], Request.Headers["APIKEY"], Request.Headers["APITOKEN"], Request.Headers["IV"], Request.Headers["ENCKEY"]);
            var resp = new SinglePaymentResponse();
            resp = await service.SinglePay(request);
            return Ok(resp);
        }

        [Route("BulkPayment")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BulkPaymentResponse))]
        public async Task<IActionResult> BulkPayment(BulkPaymentRequest request)
        {
            IntegrationService service = new IntegrationService(Request.Headers["MERCHANTID"], Request.Headers["APIKEY"], Request.Headers["APITOKEN"], Request.Headers["IV"], Request.Headers["ENCKEY"]);
            var resp = new BulkPaymentResponse();
            resp = await service.BulkPay(request);
            return Ok(resp);
        }

        [Route("BulkPaymentStatus")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BulkPaymentStatusResponse))]
        public async Task<IActionResult> BulkPaymentStatus(BulkPaymentStatusRequest request)
        {
            IntegrationService service = new IntegrationService(Request.Headers["MERCHANTID"], Request.Headers["APIKEY"], Request.Headers["APITOKEN"], Request.Headers["IV"], Request.Headers["ENCKEY"]);
            var resp = new BulkPaymentStatusResponse();
            resp = await service.BulkPayStatus(request);
            return Ok(resp);
        }

        [Route("SinglePaymentStatus")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SinglePaymentStatusResponse))]
        public async Task<IActionResult> SinglePaymentStatus(SinglePaymentStatusRequest request)
        {
            IntegrationService service = new IntegrationService(Request.Headers["MERCHANTID"], Request.Headers["APIKEY"], Request.Headers["APITOKEN"], Request.Headers["IV"], Request.Headers["ENCKEY"]);
            var resp = new SinglePaymentStatusResponse();
            resp = await service.SinglePayStatus(request);
            return Ok(resp);
        }


        [Route("AccountEnquiry")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AccountEnquiryResponse))]
        public async Task<IActionResult> AccountEnquiry(AccountEnquiryRequest request)
        {
            IntegrationService service = new IntegrationService(Request.Headers["MERCHANTID"], Request.Headers["APIKEY"], Request.Headers["APITOKEN"], Request.Headers["IV"], Request.Headers["ENCKEY"]);
            var resp = new AccountEnquiryResponse();
            resp = await service.AccountEnquiry(request);
            return Ok(resp);
        }
    }
}
