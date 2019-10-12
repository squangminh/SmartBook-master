using AdwardSoft.Core.Pattern;
using AdwardSoft.ORM.Dapper;
using AdwardSoft.Repositories.Pattern;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiContrib.Core.Formatter.Protobuf;

namespace AdwardSoft.API.Core
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
            //AdwardSoft-Nuget
            services.AddTransient<IDapperRepository, DapperRepository>();
            services.AddTransient<IAdapterPattern, AdapterPattern>();
            services.AddScoped<IGenericRepository, GenericRepository>();
            //Jwt
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            //   // options.RequireHttpsMetadata = false;
            //});



            //Config net core 2.1 with protobuf protocol
            services.AddMvc(
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddProtobufFormatters();

            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration["AuthenticationServer:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = Configuration["AuthenticationServer:ApiName"];
                });
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });           
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
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
