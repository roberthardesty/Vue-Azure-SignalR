using ipman.shared.Entity.Join;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity
{
    public class Tag : EntityBase
    {
        public string TagName { get; set; }
        public string TagImage { get; set; }
        public Guid SiteAccountID { get; set; }
        public SiteAccount SiteAccount { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedUTC { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
