﻿using ipman.shared.Entity.Join;
using ipman.shared.Entity.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ipman.shared.Entity
{
   public  class UserAccount: EntityBase
   {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GitHubID { get; set; }
        public string GoogleID { get; set; }
        public string AvatarLink { get; set; }
        public AuthenticationProvider LastLoginProvider { get; set; }
        public DateTime LastLoginUTC { get; set; }
        public DateTime CreatedUTC { get; set; }
        public DateTime LastUpdatedUTC { get; set; }
        public ICollection<SiteAccountUserAccount> SiteAccountUserAccounts { get; set; }
        [NotMapped]
        public ICollection<SiteAccount> SiteAccounts => SiteAccountUserAccounts?.Select(saua => saua.SiteAccount)?.ToList();
    }
}
