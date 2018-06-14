using ipman.core.Utilities;
using ipman.shared.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ipman.core.Query
{
    public class UserAccountGetByEmail
    {
        private IPManDataContext _ipManDataContext;

        public UserAccountGetByEmail(IPManDataContext ipManDataContext)
        {
            _ipManDataContext = ipManDataContext;
        }

        public UserAccount Execute(string email, bool includeSiteAccounts = false)
        {
            IQueryable<UserAccount> baseQuery = _ipManDataContext.UserAccounts.Where(user => user.EmailAddress == email);
            if (includeSiteAccounts)
                baseQuery = baseQuery.Include(user => user.SiteAccountUserAccounts)
                                      .ThenInclude(saua => saua.SiteAccount);
            return baseQuery.FirstOrDefault();
        }
    }
}