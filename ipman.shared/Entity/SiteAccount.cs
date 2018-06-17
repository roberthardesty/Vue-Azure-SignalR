using ipman.shared.Entity.Join;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity
{
    public class SiteAccount: EntityBase
    {
        public string SiteAccountName { get; set; }
        public string SiteAccountImagePath { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedUTC { get; set; }
        public DateTime LastUpdatedUTC { get; set; }
        public virtual ICollection<SiteAccountUserAccount> SiteAccountUserAccounts { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
