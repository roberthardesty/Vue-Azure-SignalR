using System; 
using System.Collections.Generic; 
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using ipman.core.Query;
using ipman.shared.Entity;
using System.Security.Claims;
using ipman.shared.WebServiceModels;
using IPMan.Utilities;
using ipman.core.Command;
using System.Threading.Tasks;
using ipman.shared.Utilities;
using ipman.shared.Entity.SearchCriteria;

namespace IPMan.Controllers 
{ 
    [Authorize]
    [Route("api/[controller]")] 
    public class SiteAccountController : Controller 
    { 
        private readonly SiteAccountSearch _siteAccountSearch;
        private readonly SiteAccountUpsert _siteAccountUpsert;
        public SiteAccountController(SiteAccountSearch siteAccountSearch,
                                     SiteAccountUpsert siteAccountUpsert)
        {
            _siteAccountSearch = siteAccountSearch;
            _siteAccountUpsert = siteAccountUpsert;
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

        [HttpPost("[action]")]
        public async Task<SaveSiteAccountResponse> Save([FromBody] SaveSiteAccountRequest request)
        {
            SaveSiteAccountResponse response = new SaveSiteAccountResponse();

            if (response.InitializeFromModelStateIfInvalid(ModelState))
                return response;

            if (request.ShouldUpdateAllProps)
                await _siteAccountUpsert.ExecuteAsync(request.SiteAccount);
            else
            {
                var existingSite = _siteAccountSearch.Execute(new SiteAccountSearchCriteria
                {
                    IncludedSiteAccounts = new List<Guid> { request.SiteAccount.ID }
                }).First();

                request.SiteAccount.CopyProperties(existingSite,
                    (propInfo, source, target) => request.PropsToUpdate.Contains(propInfo.Name));

                foreach (var saua in request.SiteAccountUserAccounts)
                    existingSite.SiteAccountUserAccounts.Add(saua);

                await _siteAccountUpsert.ExecuteAsync(existingSite);
            }

            return response;
        }
    } 
} 
