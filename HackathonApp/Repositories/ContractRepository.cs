using HackathonApp.Interfaces;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HackathonApp.Interfaces.Contract.H4PToken;

namespace HackathonApp.Repositories
{
    public class ContractRepository : IContract
    {
        private readonly Web3 _web3;

        private readonly string _contractAddress;

        public ContractRepository(Web3 web3, string contractAddress)
        {
            _web3 = web3;
            _contractAddress = contractAddress;
        }

        public async Task<decimal> BalanceOf(string address)
        {
            var balanceQuery = new BalanceOfFunction()
            {
                Account = address
            };
            var balanceHandler = _web3.Eth.GetContractQueryHandler<BalanceOfFunction>();
            return await balanceHandler.QueryAsync<decimal>(_contractAddress, balanceQuery);
        }

        public async Task<string> BurnToken(string address, decimal amount)
        {
            BurnFunction burnFunction = new BurnFunction()
            {
                From = address,
                Value = Web3.Convert.ToWei(amount)
            };
            var burnHandler = _web3.Eth.GetContractTransactionHandler<BurnFunction>();
            return await burnHandler.SendRequestAsync(_contractAddress, burnFunction);
        }

        public async Task<string> MintToken(string address, decimal amount)
        {
            MintFunction mintFunction = new MintFunction()
            {
                To = address,
                Value = Web3.Convert.ToWei(amount)
            };
            var mintHandler = _web3.Eth.GetContractTransactionHandler<MintFunction>();
            return await mintHandler.SendRequestAsync(_contractAddress, mintFunction);
        }
    }
}
