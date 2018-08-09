using ipman.core.Utilities;
using ipman.shared.Entity.Join;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ipman.core.Command
{
    public class SiteAccountUserAccountUpsert
    {
        private IPManDataContext _ipManDataContext;
        public SiteAccountUserAccountUpsert(IPManDataContext ipManDataContext)
        {
            _ipManDataContext = ipManDataContext;
        }

        public async Task ExecuteAsync(IEnumerable<SiteAccountUserAccount> siteAccountUserAccounts)
        {
            await _ipManDataContext.AddRangeAsync(siteAccountUserAccounts);
        }
    }
}
