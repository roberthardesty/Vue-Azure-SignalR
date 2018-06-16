using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ipman.shared.Entity.Join
{
    public class SiteAccountUserAccount: EntityBase
    {
        public Guid SiteAccountID { get; set; }
        public SiteAccount SiteAccount { get; set; }
        public Guid UserAccountID { get; set; }
        public UserAccount UserAccount { get; set; }
        public Guid RoleID { get; set; }
        public bool IsActive { get; set; }
        public bool IsMemberOfAllDepartments { get; set; }
        public DateTime LastLoginUTC { get; set; }
        [NotMapped]
        public Role Role => Role.Find(RoleID);
    }
}
