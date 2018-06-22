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
        public bool IsActive { get; set; } = true;
        public bool IsLocked { get; set; }
        public DateTime? StartTimeUTC { get; set; }
        public DateTime? LockTimeUTC { get; set; }
        public DateTime? EndTimeUTC { get; set; }
        public DateTime CreatedUTC { get; set; }
        public DateTime LastUpdatedUTC { get; set; }
        public Guid SiteAccountID { get; set; }
        public SiteAccount SiteAccount { get; set; }
        public Guid UserAccountCreatorID { get; set; }
        public UserAccount UserAccountCreator { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
        public virtual ICollection<PostWager> PostWagers { get; set; }
        public virtual ICollection<PostChoice> PostChoices { get; set; }
    }
}
