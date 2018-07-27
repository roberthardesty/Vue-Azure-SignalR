using System; 
using System.Collections.Generic; 
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using ipman.core.Query;
using ipman.shared.Entity;
using System.Security.Claims;

namespace IPMan.Controllers 
{ 
    [Authorize]
    [Route("api/[controller]")] 
    public class SiteAccountController : Controller 
    { 
        private SiteAccountGetByUserAccountEmail _siteAccountGetByUserAccountEmail;
        public SiteAccountController(SiteAccountGetByUserAccountEmail SiteAccountGetBySiteAccountName)
        {
            _siteAccountGetByUserAccountEmail = SiteAccountGetBySiteAccountName;
        }

        [HttpGet("[action]")] 
        public IEnumerable<SiteAccount> GetUserSiteAccounts() 
        {
            string email = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            List<SiteAccount> siteAccounts = _siteAccountGetByUserAccountEmail.Execute(email);
            return siteAccounts;
        } 
    } 
} 
