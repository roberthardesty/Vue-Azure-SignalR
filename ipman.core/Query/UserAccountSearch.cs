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
    public class UserAccountSearch
    {
        private IPManDataContext _dbContext;

        public UserAccountSearch(IPManDataContext ipManDataContext)
        {
            _dbContext = ipManDataContext;
        }

        public List<UserAccount> Execute(UserAccountSearchCriteria searchCriteria)
        {
            var userAccounts = (from user in _dbContext.UserAccounts
                               join saua in _dbContext.SiteAccountUserAccounts on user.ID equals saua.UserAccountID
                               join site in _dbContext.SiteAccounts on saua.SiteAccountID equals site.ID
                               where user.IsActive
                               where (searchCriteria.SiteAccountID == null
                                        || searchCriteria.SiteAccountID == site.ID)
                               where (string.IsNullOrEmpty(searchCriteria.Username)
                                        || searchCriteria.Username == user.Username)
                               where searchCriteria.IncludedUserAccounts.Count == 0
                                    || searchCriteria.IncludedUserAccounts.Contains(user.ID)
                               where searchCriteria.ExcludedUserAccounts.Count == 0
                                    || !searchCriteria.ExcludedUserAccounts.Contains(user.ID)
                               where searchCriteria.Roles.Count == 0
                                    || searchCriteria.Roles.Any(r => r.RoleID == saua.RoleID)
                               select user).Include(u => u.SiteAccountUserAccounts);

            return userAccounts.ToList();
        }
    }
}
