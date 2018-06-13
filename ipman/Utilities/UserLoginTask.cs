using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using Newtonsoft.Json.Linq;
using ipman.shared.Entity.Lookups;
using ipman.shared.Entity;
using ipman.core.Query;

namespace IPMan.Utilities
{
    public static class UserLoginTask
    {
        public static Func<OAuthCreatingTicketContext, Task> Execute(AuthenticationProvider provider)
        {
            UserAccountGetByEmail
            async Task CreateTask(OAuthCreatingTicketContext context)
            {
                // Create the request for user data
                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                // Send Request and get response
                var response = await context.Backchannel.SendAsync(request,
                    HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);

                JObject contentString = JObject.Parse(await response.Content.ReadAsStringAsync());
                string email = "";
                // Parse the response for the email
                switch (provider)
                {
                    case AuthenticationProvider.Github:
                        GetGitHubEmail(contentString, out email);
                        break;
                    case AuthenticationProvider.Google:
                        GetGoogleEmail(contentString, out email);
                        break;
                }
            }

            return CreateTask;
        }
        private static void GetGitHubEmail(JObject user, out string email)
        {
            email = "";
            if (!user.ContainsKey("email"))
                return;

            email = user["email"].ToString();
        }

        private static void GetGoogleEmail(JObject user, out string email)
        {
            email = "";
            if (!user.ContainsKey("emails"))
                return;

            JObject emailObject = user["emails"].FirstOrDefault() as JObject;

            if (emailObject == null
                || !emailObject.ContainsKey("type")
                || emailObject["type"].ToString() != "account")
                return;

            email = emailObject["value"].ToString();
        }
    }

}
//var emailClaim = new ClaimsIdentity(new[]
//{
//    new Claim("AccountWithEmail", email)
//});
//principal.AddIdentity(emailClaim);
