using System;
using System.Security.Claims;
using AspNet.Security.OAuth.GitHub;
using ipman.core.Query;
using ipman.core.Utilities;
using ipman.shared.WebServiceModels;
using IPMan.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace IPMan.Controllers
{
    [Route("/")]
    public class AuthController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly UserAccountGetByEmail _userAccountGetByEmail;
        public AuthController(IMemoryCache cache, UserAccountGetByEmail userAccountGetByEmail)
        {
            _cache = cache;
            _userAccountGetByEmail = userAccountGetByEmail;
        }

        [HttpGet("login-github")]
        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge(GitHubAuthenticationDefaults.AuthenticationScheme);
            }

            HttpContext.Response.Cookies.Append("github_username", User.Identity.Name);
            HttpContext.SignInAsync(User);
            return Redirect("/dashboard");
        }
        [HttpGet("login-google")]
        public IActionResult LoginGoogle()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge("Google");
            }

            string userSalt  = User.FindFirstValue("TempSalt");
            if (string.IsNullOrEmpty(userSalt))
                throw new Exception("User Salt Null, This Should Never Happen");

            string userToken = Guid.NewGuid().ToString();
            string userEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            _cache.Set<string>(Cryptography.Hash(userToken, userSalt), userEmail, TimeSpan.FromMinutes(30));

            HttpContext.Response.Cookies.Append("github_username", User.Identity.Name);
            HttpContext.Response.Cookies.Append("access_token_ggl", userToken, new Microsoft.AspNetCore.Http.CookieOptions());
            HttpContext.SignInAsync(User);
            return Redirect("/dashboard");
        }

        [HttpPost]
        [Authorize]
        public ValidateUserContextResponse ValidateUserContext(ValidateUserContextRequest request)
        {
            var response = new ValidateUserContextResponse();
            if (response.InitializeFromModelStateIfInvalid(ModelState))
                return response;

            string authedEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            if(authedEmail != request.Email)
            {
                response.ResponseError.ErrorType = ResponseError.ResponseErrorType.ModelValidation;
                return response;
            }

            var user = _userAccountGetByEmail.Execute(request.Email);

            if(user == null)
            {
                response.ResponseError.ErrorType = ResponseError.ResponseErrorType.ModelValidation;
                return response;
            }
            // Try to pull out a cached key using the provided token
            string hashedToken = Cryptography.Hash(request.Token, user.UserAccountSalt);
            string serverToken = _cache.Get<string>(hashedToken);
            // Even if its there we will be using a new token next time so remove this one.
            _cache.Remove(hashedToken);

            if(serverToken != user.EmailAddress)// Make sure it matches the user making the request.
            {
                response.ResponseError.ErrorType = ResponseError.ResponseErrorType.ModelValidation;
                return response;
            }

            response.NewToken = Guid.NewGuid().ToString();

            _cache.Set<string>(Cryptography.Hash(response.NewToken, user.UserAccountSalt), user.EmailAddress, TimeSpan.FromMinutes(30));

            response.UserAcount = user.RemoveSensitiveData();

            return response;
        }

    }
}    
