using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NBitcoin.BouncyCastle.Math;

namespace HackathonApp.Interfaces
{
    /// <summary>
    /// Responsible for interaction with Ethereum
    /// </summary>
    public interface IContract
    {
        /// <summary>
        /// Creates new tokens and sends them to user account
        /// </summary>
        /// <param name="address"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public Task<string> MintToken(string address, decimal amount);
        /// <summary>
        /// Removes tokens from user account and destroys them.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public Task<string> BurnToken(string address, decimal amount);
        /// <summary>
        /// Get number of tokens that the user owns.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Task<BigInteger> BalanceOf(string address);
    }
}
