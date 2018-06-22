using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity.Join
{
    public class PostWager: EntityBase
    {
        public Guid PostID { get; set; }
        public Post Post { get; set; }
        public Guid WagerID { get; set; }
        public Wager Wager { get; set; }
        public int Prediction { get; set; }
        public int? RangeLength { get; set; }
    }
}
