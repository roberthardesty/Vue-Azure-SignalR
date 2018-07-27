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
    public class SiteAccountRoleHandler : AuthorizationHandler<SiteAccountRoleRequirement, SiteAccountUserAccountRoleModel>
    {
        //private readonly UserAccountGetByEmail _userAccountGetByEmail;
        //public SiteAccountRoleHandler(UserAccountGetByEmail userAccountGetByEmail)
        //{
        //    _userAccountGetByEmail = userAccountGetByEmail;
        //}
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                             SiteAccountRoleRequirement requirement,
                                                             SiteAccountUserAccountRoleModel siteAccountUserAccountRoleModel)
        {
            UserAccount user = siteAccountUserAccountRoleModel.UserAccount;
            // await _userAccountGetByEmail.ExecuteAsync(email, true);

            SiteAccountUserAccount saua = user.SiteAccountUserAccounts.FirstOrDefault(s => s.SiteAccountID == siteAccountUserAccountRoleModel.SiteAccountID);

            if(saua == null || !requirement.RolesRequired.Contains(saua.Role))
            {
                context.Fail();
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
