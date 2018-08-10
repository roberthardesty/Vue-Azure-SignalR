using System; 
using System.Collections.Generic; 
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using ipman.core.Query;
using ipman.shared.Entity;
using System.Security.Claims;
using ipman.shared.WebServiceModels;
using ipman.core.Command;
using System.Threading.Tasks;
using IPMan.Utilities;
using ipman.shared.Utilities;
using IPMan.Authorization;

namespace IPMan.Controllers 
{ 
    [Authorize]
    [Route("api/[controller]")] 
    public class UserAccountController : Controller 
    { 
        private readonly UserAccountGetByUsername _userAccountGetByUsername;
        private readonly UserAccountGetByEmail _userAccountGetByEmail;
        private readonly UserAccountGetByID _userAccountGetByID;
        private readonly UserAccountUpsert _userAccountUpsert;
        private readonly UserAccountSearch _userAccountSearch;
        private IAuthorizationService _authorizationService;
        public UserAccountController(UserAccountGetByUsername userAccountGetByUsername,
                                     UserAccountUpsert userAccountUpsert,
                                     UserAccountGetByEmail userAccountGetByEmail,
                                     UserAccountGetByID userAccountGetByID,
                                     UserAccountSearch userAccountSearch,
                                     IAuthorizationService authorizationService)
        {
            _userAccountGetByUsername = userAccountGetByUsername;
            _userAccountUpsert = userAccountUpsert;
            _userAccountGetByEmail = userAccountGetByEmail;
            _userAccountGetByID = userAccountGetByID;
            _authorizationService = authorizationService;
            _userAccountSearch = userAccountSearch;
        }

        [HttpPost("[action]")]
        public async Task<SaveUserAccountResponse> SaveUserAccount([FromBody]SaveUserAccountRequest request)
        {
            SaveUserAccountResponse response = new SaveUserAccountResponse();

            if (response.InitializeFromModelStateIfInvalid(ModelState))
                return response;

            if (request.ShouldUpdateAllProps)
                await _userAccountUpsert.ExecuteAsync(request.UserAccount);
            else
            {
                var existingUser = await _userAccountGetByID.ExecuteAsync(request.UserAccount.ID, true);

                request.UserAccount.CopyProperties(existingUser,
                    (propInfo, source, target) => request.PropsToUpdate.Contains(propInfo.Name));

                foreach (var saua in request.SiteAccountUserAccounts)
                    existingUser.SiteAccountUserAccounts.Add(saua);

                await _userAccountUpsert.ExecuteAsync(existingUser);
            }

            return response;
        }

        [HttpPost("[action]")] 
        public async Task<SearchUserAccountsResponse> SearchUserAccounts([FromBody]SearchUserAccountsRequest request) 
        {
            var response = new SearchUserAccountsResponse();
            if (response.InitializeFromModelStateIfInvalid(ModelState))
                return response;

            if(request.UserAccountSearchCriteria.SiteAccountID.HasValue)
            {
                string email = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
                UserAccount user = await _userAccountGetByEmail.ExecuteAsync(email, true);

                AuthorizationResult authResult = await _authorizationService.AuthorizeAsync(User,
                                                                                            new SiteAccountUserAccountRoleModel { SiteAccountID = request.UserAccountSearchCriteria.SiteAccountID.Value, UserAccount = user },
                                                                                            new SiteAccountRoleRequirement(new Role[] { Role.AdminRole, Role.OwnerRole, Role.BasicRole }));
                if (!authResult.Succeeded)
                    return new SearchUserAccountsResponse { ResponseError = new ResponseError { ErrorMessage = "Not Authed" } };

            }

            response.UserAccounts = _userAccountSearch.Execute(request.UserAccountSearchCriteria)
                                                      .Select(user => user.RemoveSensitiveData());
            return response;
        }
    } 
} 
