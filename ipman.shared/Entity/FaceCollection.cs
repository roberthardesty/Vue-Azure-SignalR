using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity
{
    public class FaceCollection : EntityBase
    {
        public Guid PersonID { get; set; }
        public Person Person { get; set; }
        public List<FaceImage> FaceImages { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedUTC { get; set; }
        public DateTime LastUpdatedUTC { get; set; }
    }
}
