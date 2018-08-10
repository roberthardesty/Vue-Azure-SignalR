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
using IPMan.Authorization;

namespace IPMan.Controllers 
{ 
    [Authorize]
    [Route("api/[controller]")] 
    public class SiteAccountController : Controller 
    { 
        private readonly SiteAccountSearch _siteAccountSearch;
        private readonly SiteAccountUpsert _siteAccountUpsert;
        private UserAccountGetByEmail _userAccountGetByEmail;
        private IAuthorizationService _authorizationService;
        public SiteAccountController(SiteAccountSearch siteAccountSearch,
                                     SiteAccountUpsert siteAccountUpsert,
                                     UserAccountGetByEmail userAccountGetByEmail,
                                     IAuthorizationService authorizationService)
        {
            _siteAccountSearch = siteAccountSearch;
            _siteAccountUpsert = siteAccountUpsert;
            _userAccountGetByEmail = userAccountGetByEmail;
            _authorizationService = authorizationService;
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

                if(request.SiteAccountUserAccounts.Any(saua => saua.RoleID != Role.GuestRoleID
                                                               || saua.IsActive))
                {
                    string email = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
                    UserAccount user = await _userAccountGetByEmail.ExecuteAsync(email, true);

                    AuthorizationResult authResult = await _authorizationService.AuthorizeAsync(User,
                                                                                                new SiteAccountUserAccountRoleModel { SiteAccountID = request.SiteAccount.ID, UserAccount = user },
                                                                                                new SiteAccountRoleRequirement(new Role[] { Role.AdminRole, Role.OwnerRole, Role.BasicRole }));
                    if (!authResult.Succeeded)
                        return new SaveSiteAccountResponse { ResponseError = new ResponseError { ErrorMessage = "Not Authed" } };
                }

                foreach (var saua in request.SiteAccountUserAccounts)
                {
                    saua.SiteAccountID = existingSite.ID;
                    
                    var existingSiteUser = existingSite.SiteAccountUserAccounts.FirstOrDefault(user => user.UserAccountID == saua.UserAccountID);
                    if(existingSiteUser != null)
                    {
                        existingSite.SiteAccountUserAccounts.Remove(existingSiteUser);
                    }
                    
                    existingSite.SiteAccountUserAccounts.Add(saua);
                }

                await _siteAccountUpsert.ExecuteAsync(existingSite);
            }

            return response;
        }
    } 
} 
