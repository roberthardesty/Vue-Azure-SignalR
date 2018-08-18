using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity
{
    public class Person :  EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedUTC { get; set; }
        public DateTime LastUpdatedUTC { get; set; }
        public bool IsActive { get; set; }
        public List<FaceCollection> FaceCollections { get; set; }
    }
}
