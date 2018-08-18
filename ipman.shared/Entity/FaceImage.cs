using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity
{
    public class FaceImage : EntityBase
    {
        public Guid FaceCollectionID { get; set; }
        public FaceCollection FaceCollection {get; set;}
        public string BlobPath { get; set; }
        public DateTime CreatedUTC { get; set; }
        public bool IsActive { get; set; }
    }
}
