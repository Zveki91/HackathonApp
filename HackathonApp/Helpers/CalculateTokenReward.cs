using System;
using System.Linq;
using System.Threading.Tasks;
using HackathonApp.Data;

namespace HackathonApp.Helpers
{
    public static class CalculateTokenReward
    {
        /// <summary>
        /// Gets reward amount based on transaction pric
        /// </summary>
        /// <param name="purchase"></param>
        /// <returns></returns>
        public static async Task<int> GetRewardAmount(Purchase purchase)
        {
            int tokenCount = (int)Math.Round(purchase.Articles.Sum(x => x.Article.Price), 0) / 5;
            return purchase.Articles.Any(x => x.Article.Domestic) ? 1 + tokenCount : tokenCount;
        }
    }
}