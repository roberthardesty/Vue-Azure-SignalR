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
using ipman.core.Command;
using ipman.core.Utilities;

namespace IPMan.Utilities
{
    public static class UserLoginTask
    {
        public static Func<OAuthCreatingTicketContext, Task> Execute(AuthenticationProvider provider)
        {

            async Task CreateTask(OAuthCreatingTicketContext context)
            {
                // Create the request for user data
                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                // Send Request and get response
                var response = await context.Backchannel.SendAsync(request,
                    HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);

                JObject contentJObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                string email = "",
                       firstName = "",
                       lastName = "",
                       imageUrl = "",
                       id = "";
                // Parse the response for the email, names, and avatar url
                switch (provider)
                {
                    case AuthenticationProvider.Github:
                        GetGitHubEmail(contentJObject, out email);
                        break;
                    case AuthenticationProvider.Google:
                        GetGoogleEmail(contentJObject, out email);
                        GetGoogleNames(contentJObject, out firstName, out lastName);
                        GetGoogleAvatarLink(contentJObject, out imageUrl);
                        break;
                }
                // If an Email wasnt parsed fail out
                if(string.IsNullOrEmpty(email))
                {
                    context.Fail("No Email Provided.");
                    return;
                }
                // Check for existing user. Create if not exists. update.
                UserAccountGetByEmail userAccountGetByEmail = new UserAccountGetByEmail(new IPManDataContext(ConfigurationService.Configuration));
                UserAccountUpsert userAccountUpsert = new UserAccountUpsert(new IPManDataContext(ConfigurationService.Configuration));

                UserAccount userAccount;
                UserAccount preExistingUser = userAccountGetByEmail.Execute(email);

                if (preExistingUser == null)
                    userAccount = CreateNewUserAccount(email, firstName, lastName, imageUrl);
                else
                    userAccount = preExistingUser;

                userAccount.LastLoginUTC = DateTime.Now;
                userAccount.LastLoginProvider = provider;

                await userAccountUpsert.Execute(userAccount, preExistingUser == null);
                context.Success();
            }

            return CreateTask;
        }
        private static void GetGitHubEmail(JObject user, out string email)
        {
            email = "";
            if (user.ContainsKey("email"))
                email = user["email"].ToString();
        }
        private static void GetGoogleEmail(JObject user, out string email)
        {
            email = "";
            if (user.ContainsKey("emails"))
            {
                JObject emailObject = user["emails"].FirstOrDefault() as JObject;
                if (emailObject != null
                    && emailObject.ContainsKey("type")
                    && emailObject["type"].ToString() == "account")
                    email = emailObject["value"].ToString();
            }
        }
        private static void GetGoogleNames(JObject user, out string firstName, out string lastName)
        {
            firstName = "";
            lastName = "";
            if (!user.ContainsKey("name"))
                return;

            JObject nameObject = user["name"] as JObject;

            if(nameObject.ContainsKey("givenName") && nameObject.ContainsKey("familyName"))
            {
                firstName = nameObject["givenName"].ToString();
                lastName = nameObject["familyName"].ToString();
            }
        }
        private static void GetGoogleAvatarLink(JObject user, out string avatarLink)
        {
            avatarLink = "";
            if(user.ContainsKey("image"))
            {
                JObject imageObject = user["image"] as JObject;
                if (imageObject.ContainsKey("url"))
                    avatarLink = imageObject["url"].ToString();
            }
        }
        private static UserAccount CreateNewUserAccount(string email, string firstName, string lastName, string imageLink)
        {
            DateTime createdDate = DateTime.UtcNow;
            return new UserAccount
            {
                ID = Guid.NewGuid(),
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName,
                AvatarLink = imageLink,
                CreatedUTC = createdDate,
                LastUpdatedUTC = createdDate
            };
        }
    }

}
//var emailClaim = new ClaimsIdentity(new[]
//{
//    new Claim("AccountWithEmail", email)
//});
//principal.AddIdentity(emailClaim);
