using ipman.core.Utilities;
using ipman.shared.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipman.core.Query
{
    public class UserAccountGetByID
    {
        private IPManDataContext _ipManDataContext;

        public UserAccountGetByID(IPManDataContext ipManDataContext)
        {
            _ipManDataContext = ipManDataContext;
        }

        public async Task<UserAccount> ExecuteAsync(Guid id, bool includeSiteAccounts = false)
        {
            IQueryable<UserAccount> baseQuery = _ipManDataContext.UserAccounts.Where(user => user.ID == id);
            if (includeSiteAccounts)
                baseQuery = baseQuery.Include(user => user.SiteAccountUserAccounts)
                                      .ThenInclude(saua => saua.SiteAccount);
            return await baseQuery.FirstOrDefaultAsync();
        }
    }
}
