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

namespace IPMan.Controllers 
{ 
    [Authorize]
    [Route("api/[controller]")] 
    public class UserAccountController : Controller 
    { 
        private readonly UserAccountGetByUsername _userAccountGetByUsername;
        private readonly UserAccountGetByEmail _userAccountGetByEmail;
        private readonly UserAccountUpsert _userAccountUpsert;
        public UserAccountController(UserAccountGetByUsername userAccountGetByUsername,
                                     UserAccountUpsert userAccountUpsert,
                                     UserAccountGetByEmail userAccountGetByEmail)
        {
            _userAccountGetByUsername = userAccountGetByUsername;
            _userAccountUpsert = userAccountUpsert;
            _userAccountGetByEmail = userAccountGetByEmail;
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
                var existingUser = await _userAccountGetByEmail.ExecuteAsync(request.UserAccount.EmailAddress);

                request.UserAccount.CopyProperties(existingUser,
                    (propInfo, source, target) => request.PropsToUpdate.Contains(propInfo.Name));

                await _userAccountUpsert.ExecuteAsync(existingUser);
            }

            return response;
        }

        [HttpPost("[action]")] 
        public SearchUserAccountsResponse SearchUserAccounts([FromBody]SearchUserAccountsRequest request) 
        {
            var response = new SearchUserAccountsResponse();
            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                throw new NotImplementedException();
            }
            else if (!string.IsNullOrWhiteSpace(request.Username))
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
