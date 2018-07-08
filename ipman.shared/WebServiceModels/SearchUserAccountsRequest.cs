using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.WebServiceModels
{
    public class SearchUserAccountsRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public Guid? SiteAccountID { get; set; }
    }
}
