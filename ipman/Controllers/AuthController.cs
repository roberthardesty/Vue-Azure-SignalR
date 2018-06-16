using System.Security.Claims;
using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace IPMan.Controllers
{
    [Route("/")]
    public class AuthController : Controller
    {
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

            HttpContext.Response.Cookies.Append("github_username", User.Identity.Name);
            HttpContext.SignInAsync(User);
            return Redirect("/dashboard");
        }
    }
}    
