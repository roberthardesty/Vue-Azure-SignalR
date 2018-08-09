using ipman.core.Utilities;
using ipman.shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ipman.core.Command
{
    public class SiteAccountUpsert
    {
        private IPManDataContext _ipManDataContext;

        public SiteAccountUpsert(IPManDataContext ipManDataContext)
        {
            _ipManDataContext = ipManDataContext;
        }

        public async Task ExecuteAsync(SiteAccount siteAccount, bool isInsert = false)
        {
            if (isInsert)
            {
                _ipManDataContext.Add<SiteAccount>(siteAccount);
            }
            else
            {
                _ipManDataContext.Update<SiteAccount>(siteAccount);
            }
            await _ipManDataContext.SaveChangesAsync();
        }
    }
}
