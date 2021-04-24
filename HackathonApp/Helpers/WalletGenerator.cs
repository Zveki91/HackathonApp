using NBitcoin;
using Nethereum.HdWallet;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApp.Helpers
{
    public static class WalletGenerator
    {
        /// <summary>
        /// Creates Ethereum wallet.
        /// <remarks>Mnemonics should be returned and encypted somewhere</remarks>
        /// </summary>
        /// <returns></returns>
        public static string Create()
        {
            var memo = new Mnemonic(Wordlist.English, WordCount.Twelve);
            var wallet = new Wallet(memo.WordList, WordCount.Twelve);
            return wallet.GetAddresses(1).FirstOrDefault() ?? 
                throw new Exception("Failed to generate user wallet");
        }
    }
}
