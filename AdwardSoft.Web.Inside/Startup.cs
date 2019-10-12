using AdwardSoft.Provider.API;
using AdwardSoft.Provider.Common;
using AdwardSoft.Provider.Models;
using AdwardSoft.Provider.VStorage;
using AdwardSoft.Web.Inside.Connector;
using AdwardSoft.Web.Inside.Connector.Elastic;
using AdwardSoft.Web.Inside.Models;
using AdwardSoft.Web.Inside.TagHelpers;
using AdwardSoft.Web.Inside.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace AdwardSoft.Web.Inside
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add identity types
            //services.AddIdentity<ApplicationUser, ApplicationRole>()
            //    .AddDefaultTokenProviders();
            ////Identity Services
            //services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            //services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
            //services.AddTransient<IRoleRepository, RoleRepository>();
            //services.AddTransient<IUserRepository, UserRepository>();
            //AdwardSoft-Nuget
            //services.AddTransient<IDataGenerator, DataGenerator>();
            //services.AddTransient<IDapperRepository, DapperRepository>();
            //services.AddTransient<IAdapterPattern, AdapterPattern>();
            //Other Services
            services.AddSingleton(typeof(VStorage));
            services.AddTransient<IHttpBuilder, HttpBuilder>();
            services.AddTransient<IAPIFactory, APIFactory>();


            //SignedIn
            services.AddTransient<IUserSession, UserSession>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //AdwardSoft-HostAddress
            services.Configure<HostAddress>(Configuration.GetSection("HostAddress"));
            services.Configure<FormatImgUrl>(Configuration.GetSection("FormatImgUrl"));
            services.Configure<ConfigVStorage>(Configuration.GetSection("VStorage"));

            //ElasticSearch
            services.Configure<ElasticConnectionSettings>(Configuration.GetSection("ElasticConnectionSettings"));
            services.AddSingleton(typeof(ElasticClientProvider));
            services.AddSingleton(typeof(SQL2Es));
            services.AddSingleton(typeof(SQLEsUserDriver));

            //Default
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = "Cookies";
            //    options.DefaultChallengeScheme = "oidc";
            //})
            //    .AddCookie("Cookies")
            //    .AddOpenIdConnect("oidc", options =>
            //    {
            //        options.SignInScheme = "Cookies";

            //        options.Authority = Configuration["AuthenticationServer:Authority"];
            //        options.RequireHttpsMetadata = false;

            //        options.ClientId = "Core";
            //        options.ClientSecret = Configuration["Client:Core:Secret"];
            //        options.ResponseType = "code id_token";

            //        options.SaveTokens = true;
            //        options.GetClaimsFromUserInfoEndpoint = true;

            //        options.Scope.Add(Configuration["AuthenticationServer:ApiName"]);
            //        options.Scope.Add("offline_access");
            //    });
            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
               .AddCookie(options =>
               {
                   options.LoginPath = "/Users/Login";
                   options.LogoutPath = "/Users/Logout";
                   options.AccessDeniedPath = "/Error/AccessDenied";
                   options.ExpireTimeSpan = TimeSpan.FromHours(8);
                   options.SlidingExpiration = true;
               });

            //services.Configure<IdentityOptions>(options =>
            //{
            //    // Default Password settings.
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequiredUniqueChars = 0;
            //});

            //
            services.AddSession();
            //services.AddDistributedMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error/");
                app.UseHsts();
            }


            //app.UseRewriter(ConfigUrl.rewrite());

            app.ConfigureExceptionHandler();
            app.UseSession();

            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
