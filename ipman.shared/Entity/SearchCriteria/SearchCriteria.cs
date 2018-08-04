using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity.SearchCriteria
{
    public abstract class SearchCriteria
    {
        private IList<string> _sortBy;

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public int? RecordsToSkip => (PageNumber - 1) * PageSize;

        public IList<string> SortBy
        {
            get { return _sortBy ?? (_sortBy = new List<string>()); }
            set { _sortBy = value; }
        }
    }
}
