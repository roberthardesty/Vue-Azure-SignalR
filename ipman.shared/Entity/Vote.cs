using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity
{
    public class Vote: EntityBase
    {
        public Guid PostID { get; set; }
        public Post Post { get; set; }
        public Guid UserAccountID { get; set; }
        public UserAccount UserAccount { get; set; }
        public DateTime CreatedUTC { get; set; }
        public bool IsUpTally { get; set; }
        public bool IsComment { get; set; }
    }
}
