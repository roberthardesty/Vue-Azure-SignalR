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
        public virtual ICollection<PostTag> PostTag { get; set; }
    }
}
