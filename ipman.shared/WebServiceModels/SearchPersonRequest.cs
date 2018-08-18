using ipman.shared.Entity.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.WebServiceModels
{
    public class SearchPersonRequest
    {
        public PersonSearchCriteria SearchCriteria { get; set; }
    }
}
