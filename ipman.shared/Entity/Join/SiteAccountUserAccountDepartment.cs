using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ipman.shared.Entity.Join
{
    public class SiteAccountUserAccountDepartment
    {
        public bool IsActive { get; set; }
        public DateTime CreatedUTC { get; set; }
        public Guid SiteAccountUserAccountID { get; set; }
        public SiteAccountUserAccount SiteAccountUser { get; set; }
        public Guid DepartmentID { get; set; }
        public Department Department { get; set; }
    }
}
