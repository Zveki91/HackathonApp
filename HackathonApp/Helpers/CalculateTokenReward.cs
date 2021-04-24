using System;
using System.Linq;
using System.Threading.Tasks;
using HackathonApp.Data;

namespace HackathonApp.Helpers
{
    public static class CalculateTokenReward
    {
        public static async Task<int> GetRewardAmount(Purchase purchase)
        {

            var domestic = purchase.Articles
                .Select(x => x.Article.Domestic == true).ToList().Count;

            return domestic + ((int)Math.Round(purchase.Articles.Sum(x => x.Article.Price),0) / 5);
            
        }
    }
}