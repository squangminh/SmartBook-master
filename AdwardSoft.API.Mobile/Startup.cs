using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdwardSoft.API.Mobile.Connector;
using AdwardSoft.API.Mobile.Connector.SingalR;
using AdwardSoft.API.Mobile.Helper;
using AdwardSoft.API.Mobile.Model;
using AdwardSoft.Core.Pattern;
using AdwardSoft.DTO.Provider.Google.Firebase;
using AdwardSoft.ORM.Dapper;
using AdwardSoft.Repositories.Pattern;
using AdwardSoft.Web.Inside.Connector.Elastic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AdwardSoft.API.Mobile
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
            // Register the client provider as a singleton
            services.Configure<ElasticConnectionSettings>(Configuration.GetSection("ElasticConnectionSettings"));
            services.AddSingleton(typeof(ElasticClientProvider));
            services.AddSingleton(typeof(TicketHelper));
            services.AddSingleton(typeof(SQL2Es));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .AllowAnyOrigin();
                //.WithOrigins("https://localhost:44337");
            }));

            //SingalR
            services.AddSignalR();

            //Config net core 2.1 with protobuf protocol
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

            services.Configure<VNPAYSettings>(Configuration.GetSection("VNPAY"));
            //services.Configure<FirebaseCloudMessage>(Configuration.GetSection("FCM"));
            services.Configure<EmailPartner>(Configuration.GetSection("EmailPartner"));
            services.Configure<EmailCustomer>(Configuration.GetSection("EmailCustomer"));
            services.Configure<FirebaseCloudMessage>(Configuration.GetSection("FCM"));

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

            app.UseCors("CorsPolicy");
            // global cors policy
            //app.UseCors(x => x
            //    .AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .AllowCredentials());
            //app.UseHttpsRedirection();
            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<UserDriverHub>("/QBPlusHub");
            //});
            app.UseAuthentication();
            app.UseStaticFiles();
            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<VNPayReturnHub>("/vnpReturnHub");
            //});

            app.UseMvc();
        }
    }
}
