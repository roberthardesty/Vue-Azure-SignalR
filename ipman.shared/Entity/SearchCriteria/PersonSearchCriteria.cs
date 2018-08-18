using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity.SearchCriteria
{
    public class PersonSearchCriteria: SearchCriteria
    {
        public List<Guid> IncludedPersonIDs { get; set; }
        public List<Guid> ExcludedPersonIDs { get; set; }
        public string Keyword { get; set; }
        public Guid CommunityID { get; set; }
    }
}
