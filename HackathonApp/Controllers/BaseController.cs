using System;
using System.Linq;
using System.Security.Claims;
using HackathonApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.NonceServices;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace HackathonApp.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        private Web3 _web3Cache = null;
        private Web3 _web3
        {
            get
            {
                if (_web3Cache != null)
                    return _web3Cache;

                RpcClient rpcClient = new RpcClient(new Uri(Configuration["NodeUrl"]));
                Account account = new Account(Configuration["PrivateKey"], Chain.Ropsten);
                account.NonceService = new InMemoryNonceService(account.Address, rpcClient);
                _web3Cache = new Web3(account, rpcClient);
                return _web3;
            }
        }

        /// <summary>
        /// Constructor for base controller
        /// </summary>
        /// <param name="configuration"></param>
        public BaseController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// User Id from JwtToken
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Email from JwtToken
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Role from JwtToken
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// Configuration that allows access to JWT decoding
        /// </summary>
        protected IConfiguration Configuration { get; }

        protected ContractRepository ContractService =>
            new ContractRepository(_web3, Configuration["ContractAddress"]);

        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            (Role) = (actionContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value);
            (UserId) = Guid.Parse((actionContext.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value) ?? "00000000-0000-0000-0000-000000000000");
        }
    }
}