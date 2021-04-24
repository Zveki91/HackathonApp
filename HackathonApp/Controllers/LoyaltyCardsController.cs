using HackathonApp.Dto;
using HackathonApp.Helpers;
using HackathonApp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApp.Controllers
{
    [Route("api/loyalty-cards")]
    [ApiController]
    public class LoyaltyCardController : BaseController
    {
        private readonly IPurchase _purchase;

        public LoyaltyCardController(IConfiguration configuration, IPurchase purchase) : base(configuration) 
        {
            _purchase = purchase;
        }

        /// <summary>
        /// Invoked on purchase on PoS terminals.
        /// </summary>
        /// <param name="purchaseRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("submit")]
        public async Task<ActionResult<object>> SubmitPurchaseAsync([FromBody]CreatePurchaseDto purchaseRequest)
        {
            purchaseRequest.CustomerId = UserId;
            return Ok(await _purchase.CreatePurchase(purchaseRequest));
        }

        /// <summary>
        /// Invoked on purchase when loyalty tokens are used.
        /// </summary>
        /// <param name="redeemRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("redeem")]
        public ActionResult<object> RedeemTokens([FromBody]object redeemRequest)
        {
            return Ok();
        }
    }
}
