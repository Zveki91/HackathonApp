using System;
using System.Collections.Generic;

namespace HackathonApp.Dto
{
    public class PurchaseDto
    {
        public Guid BranchId { get; set; }
        public Guid UserId { get; set; }

        public List<Guid> Article { get; set; }

    }
}