using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity
{
   public  class UserAccount: EntityBase
   {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastLoginUTC { get; set; }
        public DateTime CreatedUTC { get; set; }
        public DateTime LastUpdatedUTC { get; set; }
    }
}
