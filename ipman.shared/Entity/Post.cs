using ipman.shared.Entity.Join;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity
{
    public class Post : EntityBase
    {
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }
        public Guid SiteAccountID { get; set; }
        public SiteAccount SiteAccount { get; set; }
        public Guid UserAccountCreatorID { get; set; }
        public UserAccount UserAccountCreator { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<PostTag> PostTag { get; set; }
    }
}
