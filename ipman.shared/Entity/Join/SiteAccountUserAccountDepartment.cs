using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity.Join
{
    public class SiteAccountUserAccountDepartment: EntityBase
    {
        public Guid SiteAccountUserAccountID { get; set; }
        public ICollection<SiteAccountUserAccount> SiteAccountUserAccounts { get; set; }
        public Guid DepartmentID { get; set; }
        public Department Department { get; set; }
    }
}
