using HackathonApp.Helpers;
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
        public LoyaltyCardController(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Invoked on purchase on PoS terminals.
        /// </summary>
        /// <param name="purchaseRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("submit")]
        public ActionResult<object> SubmitPurchase([FromBody]object purchaseRequest)
        {
            return Ok();
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
