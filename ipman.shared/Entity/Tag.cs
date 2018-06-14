using ipman.shared.Entity.Join;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity
{
    public class Tag : EntityBase
    {
        public string TagName { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}
