using ipman.core.Utilities;
using ipman.shared.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ipman.core.Query
{
    public class UserAccountGetByUsername
    {
        private IPManDataContext _ipManDataContext;

        public UserAccountGetByUsername(IPManDataContext ipManDataContext)
        {
            _ipManDataContext = ipManDataContext;
        }

        public IEnumerable<UserAccount> Execute(string username, bool includeSiteAccounts = false)
        {
            IQueryable<UserAccount> baseQuery = _ipManDataContext.UserAccounts.Where(user => user.Username == username);
            if (includeSiteAccounts)
                baseQuery = baseQuery.Include(user => user.SiteAccountUserAccounts)
                                      .ThenInclude(saua => saua.SiteAccount);

            return baseQuery.ToList();
        }
    }
}
