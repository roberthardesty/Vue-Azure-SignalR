using ipman.shared.Entity;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPMan.Authorization
{
    public class SiteAccountRoleRequirement: IAuthorizationRequirement
    {
        public Role[] RolesRequired { get; }
        public SiteAccountRoleRequirement(Role[] rolesRequired)
        {
            RolesRequired = rolesRequired;
        }
    }
}
