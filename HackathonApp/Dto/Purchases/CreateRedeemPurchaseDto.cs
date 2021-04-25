using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonApp.Dto.Purchases
{
    public class CreateRedeemPurchaseDto : CreatePurchaseDto
    {
        public int TokenAmount { get; set; }
    }
}
