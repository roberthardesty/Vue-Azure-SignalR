using ipman.core.Utilities;
using ipman.shared.Entity;
using ipman.shared.Entity.SearchCriteria;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ipman.core.Query
{
    public class SiteAccountSearch
    {
        private IPManDataContext _dbContext;

        public SiteAccountSearch(IPManDataContext ipManDataContext)
        {
            _dbContext = ipManDataContext;
        }

        public List<SiteAccount> Execute(SiteAccountSearchCriteria searchCriteria)
        {
            var siteAccounts = from site in _dbContext.SiteAccounts
                               join saua in _dbContext.SiteAccountUserAccounts on site.ID equals saua.SiteAccountID
                               join user in _dbContext.UserAccounts on saua.UserAccountID equals user.ID
                               where (searchCriteria.CurrentUserSites && searchCriteria.OtherUserSites)
                                      || (user.EmailAddress == searchCriteria.UserEmail && searchCriteria.CurrentUserSites)
                                      || (user.EmailAddress != searchCriteria.UserEmail && searchCriteria.OtherUserSites)
                               where string.IsNullOrWhiteSpace(searchCriteria.Keyword)
                                      || site.SiteAccountName.Contains(searchCriteria.Keyword)
                               where searchCriteria.ExcludedSiteAccounts.Count == 0
                                      || !searchCriteria.ExcludedSiteAccounts.Contains(site.ID)
                               select site;

            if (searchCriteria.IncludeSiteAccountUserAccounts)
                siteAccounts.Include((sa) => sa.SiteAccountUserAccounts);

            return siteAccounts.ToList();
        }
    }
}
