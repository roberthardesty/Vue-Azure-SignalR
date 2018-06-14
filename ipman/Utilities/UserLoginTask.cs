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

                UserAccount userAccount = new UserAccount();
                // Parse the response for the email, names, and avatar url
                switch (provider)
                {
                    case AuthenticationProvider.Github:
                        userAccount.AddGitHubEmailAddress(contentJObject);
                        break;
                    case AuthenticationProvider.Google:
                        userAccount.AddGoogleEmail(contentJObject)
                                   .AddGoogleNames(contentJObject)
                                   .AddGoogleAvatarLink(contentJObject);
                        break;
                }
                // If an Email wasnt parsed fail out
                if(string.IsNullOrEmpty(userAccount.EmailAddress))
                {
                    context.Fail("No Email Provided.");
                    return;
                }
                // Check for existing user. Create if not exists. update.
                UserAccountGetByEmail userAccountGetByEmail = new UserAccountGetByEmail(new IPManDataContext(ConfigurationService.Configuration));
                UserAccountUpsert userAccountUpsert = new UserAccountUpsert(new IPManDataContext(ConfigurationService.Configuration));

                UserAccount preExistingUser = userAccountGetByEmail.Execute(userAccount.EmailAddress);

                if (preExistingUser == null)
                    userAccount.AddCreatedData();
                else
                    userAccount = preExistingUser;

                userAccount.AddLoginData(provider);

                await userAccountUpsert.Execute(userAccount, preExistingUser == null);
                context.Success();
            }

            return CreateTask;
        }

        #region Parsing User Data

        private static UserAccount AddGitHubEmailAddress(this UserAccount userAccount, JObject user)
        {
            if (user.ContainsKey("email"))
                userAccount.EmailAddress = user["email"].ToString();
            return userAccount;
        }
        private static UserAccount AddGoogleEmail(this UserAccount userAccount, JObject user)
        {
            if (user.ContainsKey("emails"))
            {
                JObject emailObject = user["emails"].FirstOrDefault() as JObject;
                if (emailObject != null
                    && emailObject.ContainsKey("type")
                    && emailObject["type"].ToString() == "account")
                    userAccount.EmailAddress = emailObject["value"].ToString();
            }
            return userAccount;
        }
        private static UserAccount AddGoogleNames(this UserAccount userAccount, JObject user)
        {
            if (user.ContainsKey("name"))
            {
                JObject nameObject = user["name"] as JObject;

                if(nameObject.ContainsKey("givenName") && nameObject.ContainsKey("familyName"))
                {
                    userAccount.FirstName = nameObject["givenName"].ToString();
                    userAccount.LastName = nameObject["familyName"].ToString();
                }
            }
            return userAccount;
        }
        private static UserAccount AddGoogleAvatarLink(this UserAccount userAccount, JObject user)
        {
            if(user.ContainsKey("image"))
            {
                JObject imageObject = user["image"] as JObject;
                if (imageObject.ContainsKey("url"))
                    userAccount.AvatarLink = imageObject["url"].ToString();
            }
            return userAccount;
        }
#endregion
        private static UserAccount AddCreatedData(this UserAccount userAccount)
        {
            DateTime createdDate = DateTime.UtcNow;
            userAccount.CreatedUTC = createdDate;
            userAccount.LastUpdatedUTC = createdDate;
            return userAccount;
        }
        private static UserAccount AddLoginData(this UserAccount userAccount, AuthenticationProvider provider)
        {
            DateTime nowTime = DateTime.UtcNow;
            userAccount.LastLoginUTC = nowTime;
            userAccount.LastLoginProvider = provider;
            return userAccount;
        }
    }

}

