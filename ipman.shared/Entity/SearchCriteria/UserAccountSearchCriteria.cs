using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity.SearchCriteria
{
    public class UserAccountSearchCriteria: SearchCriteria
    {
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public bool IncludeSiteAccountUserAccounts { get; set; }
        public Guid? SiteAccountID { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<Guid> ExcludedUserAccounts { get; set; } = new List<Guid>();
        public List<Guid> IncludedUserAccounts { get; set; } = new List<Guid>();
    }
}
