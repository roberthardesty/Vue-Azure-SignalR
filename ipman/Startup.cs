using System;
using System.Linq;
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
using IPMan.Utilities;
using ipman.shared.Entity.Lookups;
using ipman.core.Utilities;
using Microsoft.AspNetCore.Authentication;
using ipman.core.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ipman.core.Command;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using IPMan.Authorization;

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
            ConfigurationService.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMemoryCache();
            services.AddMvc().AddJsonOptions(options => 
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; 
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddSignalR().AddAzureSignalR().AddJsonProtocol(p => 
            {
                p.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                p.PayloadSerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            ConfigureAuthentication(services);
            ConfigureAuthorization(services);

            services.AddSingleton<IAuthorizationHandler, SiteAccountRoleHandler>();

            services.AddSingleton<IHostedService, Counter>();
            services.AddSingleton<IHostedService, Weather>();

            services.AddTransient<UserAccountUpsert>();
            services.AddTransient<PostUpsert>();
            services.AddTransient<PostGetBySiteAccountName>();
            services.AddTransient<PostGetBySiteAccountID>();
            services.AddTransient<UserAccountGetByEmail>();
            services.AddTransient<UserAccountGetByUsername>();
            services.AddTransient<SiteAccountGetByUserAccountEmail>();
            
            services.AddDbContext<IPManDataContext>();
        }
        public void ConfigureAuthentication(IServiceCollection services)
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
                            OnCreatingTicket = UserLoginTaskFactory.New(AuthenticationProvider.Github)
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
                            OnCreatingTicket = UserLoginTaskFactory.New(AuthenticationProvider.Google)
                        };
                        //googleOptions.ClaimActions.MapJsonKey("urn:google:image","image");
                        googleOptions.ClaimActions.Remove(ClaimTypes.GivenName);
                    });
        }
        public void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Microsoft_Only", policy => policy.RequireClaim("Company", "Microsoft"));
            });
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors((builder) => 
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
                
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
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
                routes.MapHub<PiCamHub>("/PiCam");
                routes.MapHub<PostHub>("/Post");
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
        
    }
}
