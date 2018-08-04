using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity.SearchCriteria
{
    public class SiteAccountSearchCriteria: SearchCriteria
    {
        public string Keyword { get; set; }
        public bool OtherUserSites { get; set; }
        public bool CurrentUserSites { get; set; }
        public string UserEmail { get; set; }
        public bool IncludeSiteAccountUserAccounts { get; set; }
        public List<Guid> ExcludedSiteAccounts { get; set; }
    }
}
