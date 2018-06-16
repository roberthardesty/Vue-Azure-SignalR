using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity.Join
{
    public class PostTag: EntityBase
    {
        public Guid PostID { get; set; }
        public Post Post { get; set; }
        public Guid TagID { get; set; }
        public Tag Tag { get; set; }
    }
}
