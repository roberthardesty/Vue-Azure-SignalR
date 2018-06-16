using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ipman.core.Query;
using ipman.shared.Entity;
using ipman.shared.Entity.Join;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;


namespace IPMan.Services.Hubs
{
    [Authorize]
    public class SiteAccountHub : Hub
    {
        private UserAccountGetByEmail _userAccountGetByEmail;
        public SiteAccountHub(UserAccountGetByEmail userAccountGetByEmail)
        {
            _userAccountGetByEmail = userAccountGetByEmail;
        }
        public List<SiteAccount> GetUserSiteAccounts()
        {
            string email = Context.User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            UserAccount user = _userAccountGetByEmail.Execute(email, true);

            if(user.SiteAccountUserAccounts == null)
                user.SiteAccountUserAccounts = new List<SiteAccountUserAccount>();

            return user.SiteAccounts.ToList();
        }
    }
}
