using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ipman.shared.Entity;
using ipman.shared.Entity.Join;

namespace ipman.shared.WebServiceModels
{
    public class SaveSiteAccountRequest
    {
        [Required]
        public SiteAccount SiteAccount { get; set; }
        public List<SiteAccountUserAccount> SiteAccountUserAccounts { get; set; }
        public bool ShouldUpdateAllProps { get; set; }
        public List<string> PropsToUpdate { get; set; }
    }
}
