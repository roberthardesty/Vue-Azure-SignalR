using ipman.shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.WebServiceModels
{
    public class SearchUserAccountsResponse: BaseResponse
    {
        public IEnumerable<UserAccount> UserAccounts { get; set; }
    }
}
