using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity
{
    public class PostChoice: EntityBase
    {
        public string ChoiceName { get; set; }
        public int ChoiceValue { get; set; }
        public int Order { get; set; }
        public Guid PostID { get; set; }
        public Post Post { get; set; }
    }
}
