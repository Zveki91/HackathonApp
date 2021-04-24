using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyCardController : BaseController
    {
        public LoyaltyCardController(IConfiguration configuration) : base(configuration) { }

        [HttpPost]
        public ActionResult<object> SubmitPurchase([FromBody]object purchaseRequest)
        {
            // Get user from database.
            // ContractService.MintToken(user.address, purchaseRequest.amount);
            return Ok();
        }

        [HttpPost]
        public ActionResult<object> RedeemTokens([FromBody]object redeemRequest)
        {
            return Ok();
        }
    }
}
