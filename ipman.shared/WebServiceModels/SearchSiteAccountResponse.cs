using ipman.shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.WebServiceModels
{
    public class SearchSiteAccountResponse: BaseResponse
    {
        public List<SiteAccount> SiteAccounts { get; set; }
    }
}
