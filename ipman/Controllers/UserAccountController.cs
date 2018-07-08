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
    public class UserAccountController : Controller 
    { 
        private UserAccountGetByUsername _userAccountGetByUsername;
        public UserAccountController(UserAccountGetByUsername userAccountGetByUsername)
        {
            _userAccountGetByUsername = userAccountGetByUsername;
        }

        [HttpPost("[action]")] 
        public SearchUserAccountsResponse SearchUserAccounts(SearchUserAccountsRequest request) 
        {
            var response = new SearchUserAccountsResponse();
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new NotImplementedException();
            }
            else if (string.IsNullOrWhiteSpace(request.Username))
            {
                response.UserAccounts = _userAccountGetByUsername.Execute(request.Username);
            }
            else if (request.SiteAccountID.HasValue)
            {
                throw new NotImplementedException();
            }
            return response;
        } 
    } 
} 
