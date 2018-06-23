using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity.Join
{
    public class PostTag
    {
        public Guid PostID { get; set; }
        public Post Post { get; set; }
        public Guid TagID { get; set; }
        public Tag Tag { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedUTC { get; set; }
    }
}
