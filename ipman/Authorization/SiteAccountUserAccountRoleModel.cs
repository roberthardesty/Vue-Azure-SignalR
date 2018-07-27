using ipman.shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPMan.Authorization
{
    public class SiteAccountUserAccountRoleModel
    {
        public Guid SiteAccountID { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}
