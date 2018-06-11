using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity.Join
{
    public class SiteAccountUserAccountDepartment
    {
        public Guid SiteAccountUserAccountID { get; set; }
        public SiteAccountUserAccount SiteAccountUserAccount { get; set; }
        public Guid DepartmentID { get; set; }
        public Department Department { get; set; }
    }
}
