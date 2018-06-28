using ipman.core.Utilities;
using ipman.shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ipman.core.Query
{
    public class SiteAccountGetByUserAccountEmail
    {
        private IPManDataContext _dbContext;

        public SiteAccountGetByUserAccountEmail(IPManDataContext ipManDataContext)
        {
            _dbContext = ipManDataContext;
        }

        public List<SiteAccount> Execute(string email)
        {
            var sites  = _dbContext.SiteAccounts.Where(sa => sa.SiteAccountUserAccounts.Any(saua => saua.UserAccount.EmailAddress == email));
            return sites.ToList();
        }
    }
}
