using ipman.shared.Entity.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.WebServiceModels
{
    public class SearchUserAccountsRequest
    {
        public UserAccountSearchCriteria UserAccountSearchCriteria { get; set; }
    }
}
