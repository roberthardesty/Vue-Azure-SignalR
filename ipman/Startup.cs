using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IPMan.Services.Hubs;
using System.Threading.Tasks;

namespace IPMan
{
    public class Startup
    {
        private const string GitHubClientId = "GitHubClientId";
        private const string GitHubClientSecret = "GitHubClientSecret";
        private const string GoogleClientId = "GoogleClientId";
        private const string GoogleClientSecret = "GoogleClientSecret";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSignalR().AddAzureSignalR();
            ConfigureAuthorization(services);
            services.AddSingleton<IHostedService, Counter>();
            services.AddSingleton<IHostedService, Weather>();
        }

        public void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie()
                    .AddGitHub(options =>
                    {
                        options.ClientId = Configuration[GitHubClientId];
                        options.ClientSecret = Configuration[GitHubClientSecret];
                        options.Scope.Add("user:email");
                        options.Events = new OAuthEvents
                        {
                            OnCreatingTicket = GetUserCompanyInfoAsync
                        };
                    })
                    .AddGoogle("Google", googleOptions =>
                    {
                        googleOptions.ClientId = Configuration[GoogleClientId];
                        googleOptions.ClientSecret = Configuration[GoogleClientSecret];
                        googleOptions.AuthorizationEndpoint += "?prompt=consent"; // Hack so we always get a refresh token, it only comes on the first authorization response
                        googleOptions.AccessType = "offline";
                        googleOptions.SaveTokens = true;
                        googleOptions.Events = new OAuthEvents
                        {
                            OnCreatingTicket = GetUserCompanyInfoAsync
                        };
                        //googleOptions.ClaimActions.MapJsonSubKey("urn:google:image", "image", "url");
                        googleOptions.ClaimActions.Remove(ClaimTypes.GivenName);
                    });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseAuthentication();
            app.UseStaticFiles();            
            app.UseAzureSignalR(routes =>
            {
                routes.MapHub<CounterHub>("/count");
                routes.MapHub<WeatherHub>("/weather");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/login"),
                            builder =>
            {
                builder.UseMvc(routes =>
                {
                    routes.MapSpaFallbackRoute(
                        name: "spa-fallback",
                        defaults: new { controller = "Home", action = "Index" });
                });
            });
        }
        private static async Task GetUserCompanyInfoAsync(OAuthCreatingTicketContext context)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

            var response = await context.Backchannel.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);

            var user = JObject.Parse(await response.Content.ReadAsStringAsync());
            if (user.ContainsKey("company"))
            {
                var company = user["company"].ToString();
                var companyIdentity = new ClaimsIdentity(new[]
                {
                    new Claim("Company", company)
                });
                context.Principal.AddIdentity(companyIdentity);
            }
        } 
    }
}
