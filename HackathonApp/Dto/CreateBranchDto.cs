using System;

namespace HackathonApp.Dto
{
    public class CreateBranchDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }

        public Guid CompanyId { get; set; }

        public Guid BranchManagerId { get; set; }
    }
}