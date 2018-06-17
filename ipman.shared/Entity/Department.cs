using ipman.shared.Entity.Join;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity
{
    public class Department: EntityBase
    {
        public string DepartmentName { get; set; }
        public DateTime CreatedUTC { get; set; }
        public DateTime LastUpdatedUTC { get; set; }
        public bool IsActive { get; set; }
        public Guid SiteAccountID { get; set; }
        public SiteAccount SiteAccount { get; set; }
        public ICollection<SiteAccountUserAccountDepartment> SiteAccountUserAccountDepartments { get; set; }
    }
}
