using ipman.shared.Entity.Join;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity
{
    public class Wager: EntityBase
    {
        public Guid UserAccountID { get; set; }
        public UserAccount UserAccount { get; set; }
        public int WagerAmount { get; set; }
        public virtual ICollection<PostWager> PostWagers { get; set; }
    }
}
