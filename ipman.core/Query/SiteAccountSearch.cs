﻿using ipman.core.Utilities;
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
            var siteAccounts = (from site in _dbContext.SiteAccounts
                               join saua in _dbContext.SiteAccountUserAccounts on site.ID equals saua.SiteAccountID
                               join user in _dbContext.UserAccounts on saua.UserAccountID equals user.ID
                               where site.IsActive
                                      &&  (!searchCriteria.CurrentUserSites && !searchCriteria.OtherUserSites)
                                      || (searchCriteria.CurrentUserSites && user.EmailAddress == searchCriteria.UserEmail)
                                      || (searchCriteria.OtherUserSites && user.EmailAddress != searchCriteria.UserEmail)
                               where string.IsNullOrWhiteSpace(searchCriteria.Keyword)
                                      || site.SiteAccountName.Contains(searchCriteria.Keyword)
                               where searchCriteria.ExcludedSiteAccounts.Count == 0
                                      || !searchCriteria.ExcludedSiteAccounts.Contains(site.ID)
                               where searchCriteria.IncludedSiteAccounts.Count == 0
                                      || searchCriteria.IncludedSiteAccounts.Contains(site.ID)
                                select site).Include(site => site.SiteAccountUserAccounts);

            return siteAccounts.ToList();
        }
    }
}
