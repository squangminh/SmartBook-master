using System;
using AdwardSoft.API.Authentication.Configurations.IdentityServer;
using AdwardSoft.API.Authentication.Model;
using AdwardSoft.Core.Identity;
using AdwardSoft.Core.Pattern;
using AdwardSoft.DTO.Identity;
using AdwardSoft.ORM.Dapper;
using AdwardSoft.Provider.Models;
using AdwardSoft.Repositories.Identity;
using AdwardSoft.Repositories.Pattern;
using AutoMapper;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiContrib.Core.Formatter.Protobuf;

namespace AdwardSoft.API.Authentication
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
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddDefaultTokenProviders();
            //Identity Services
            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            //services.AddDefaultIdentity<ApplicationUser>(config =>
            //{
            //    config.SignIn.RequireConfirmedEmail = true;
            //});
            //AdwardSoft-Nuget
            services.AddTransient<IDapperRepository, DapperRepository>();
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<IAdapterPattern, AdapterPattern>();
            services.AddScoped<IGenericRepository, GenericRepository>();


            //Jwt
            //services.AddAuthentication(option => {
            //    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //    option.DefaultSignInScheme = IdentityServerConstants.DefaultCookieAuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = Configuration["Jwt:Issuer"],
            //        ValidAudience = Configuration["Jwt:Issuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            //    };
            //});

            //Application Cookie
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.AccessDeniedPath = "/Users/AccessDenied";
            //    options.Cookie.Name = "Okaylah";
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //    options.LoginPath = "/Users/Login";
            //    // ReturnUrlParameter requires 
            //    //using Microsoft.AspNetCore.Authentication.Cookies;
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //    options.SlidingExpiration = true;
            //});

            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.Configure<DataProtectionTokenProviderOptions>(o =>
                o.TokenLifespan = TimeSpan.FromHours(3));

            services.Configure<IdentityOptions>(options =>
            {
                // Default User settings.
                options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
                
            });

            //AutoMapper
            services.AddAutoMapper();

            

            services
                .AddIdentityServer()
                .AddDeveloperSigningCredential()
            //  .AddInMemoryPersistedGrants()
              //.AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients(Configuration))
                .AddAspNetIdentity<ApplicationUser>();
            //http://localhost:5000/.well-known/openid-configuration

            services.Configure<ImageResources>(Configuration.GetSection("ImageResources"));
            services.Configure<ImagePath>(Configuration.GetSection("ImagePath"));
            services.Configure<ExternalProvider>(Configuration.GetSection("ExternalProvider"));
            services.Configure<MailOpt>(Configuration.GetSection("MailOpt"));
            //services.Configure<FirebaseCloudMessage>(Configuration.GetSection("FCM"));


            //services.AddAuthentication().AddGoogle(googleOptions =>
            //{
            //    googleOptions.ClientId = "662556414868-fqh8vnehnvitbbvbi8iqtk557h6uulcs.apps.googleusercontent.com";//Configuration["Authentication:Google:ClientId"];
            //    googleOptions.ClientSecret = "wr62BaR_Tstx17V_SwWCx9AZ";// Configuration["Authentication:Google:ClientSecret"];
            //});

            services.AddAuthentication()
            .AddGoogle("Google", options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.ClientId = "662556414868-fqh8vnehnvitbbvbi8iqtk557h6uulcs.apps.googleusercontent.com";
                options.ClientSecret = "wr62BaR_Tstx17V_SwWCx9AZ";
            });


            //Config net core 2.1 with protobuf protocol
            services.AddMvc(
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddProtobufFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // global cors policy
            //app.UseCors(x => x
            //    .AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .AllowCredentials());
            
            app.UseStaticFiles();
            app.UseIdentityServer();
            //app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
