using ipman.core.Query;
using ipman.shared.Entity;
using ipman.shared.Entity.Join;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IPMan.Authorization
{
    public class SiteAccountRoleHandler : AuthorizationHandler<SiteAccountRoleRequirement, SiteAccount>
    {
        private readonly UserAccountGetByEmail _userAccountGetByEmail;
        public SiteAccountRoleHandler(UserAccountGetByEmail userAccountGetByEmail)
        {
            _userAccountGetByEmail = userAccountGetByEmail;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SiteAccountRoleRequirement requirement, SiteAccount site)
        {
            string email = context.User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrWhiteSpace(email))
            {
                context.Fail();
                return;
            }

            UserAccount user = await _userAccountGetByEmail.ExecuteAsync(email, true);

            SiteAccountUserAccount saua = user.SiteAccountUserAccounts.FirstOrDefault(s => s.SiteAccountID == site.ID);

            if(saua == null || !requirement.RolesRequired.Contains(saua.Role))
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
    }
}
