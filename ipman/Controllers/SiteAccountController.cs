using System; 
using System.Collections.Generic; 
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using ipman.core.Query;
using ipman.shared.Entity;
using System.Security.Claims;
using ipman.shared.WebServiceModels;

namespace IPMan.Controllers 
{ 
    [Authorize]
    [Route("api/[controller]")] 
    public class SiteAccountController : Controller 
    { 
        private readonly SiteAccountGetByUserAccountEmail _siteAccountGetByUserAccountEmail;
        private readonly SiteAccountSearch _siteAccountSearch;
        public SiteAccountController(SiteAccountGetByUserAccountEmail siteAccountGetBySiteAccountName,
                                     SiteAccountSearch siteAccountSearch)
        {
            _siteAccountGetByUserAccountEmail = siteAccountGetBySiteAccountName;
            _siteAccountSearch = siteAccountSearch;
        }

        [HttpGet("[action]")] 
        public IEnumerable<SiteAccount> GetUserSiteAccounts() 
        {
            string email = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            List<SiteAccount> siteAccounts = _siteAccountGetByUserAccountEmail.Execute(email);
            return siteAccounts;
        }

        [HttpPost("[action]")]
        public SearchSiteAccountResponse Search([FromBody] SearchSiteAccountRequest request)
        {
            SearchSiteAccountResponse searchResponse = new SearchSiteAccountResponse
            {
                SiteAccounts = new List<SiteAccount>(),
            };

            string email = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            request.SiteAccountSearchCriteria.UserEmail = email;

            searchResponse.SiteAccounts = _siteAccountSearch.Execute(request.SiteAccountSearchCriteria);

            return searchResponse;
        }
    } 
} 
