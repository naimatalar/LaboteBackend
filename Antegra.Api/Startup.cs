using Labote.Core;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.SqlServer;
using Labote.Api.BackgroundJobs;
using Labote.Services;
using Microsoft.AspNetCore.Identity;
using Labote.Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Labote.Core.Constants;
using Microsoft.Extensions.FileProviders;
using System.IO;


namespace Labote.Api
{
    public class Startup
    {

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }


        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
             .AddRazorRuntimeCompilation();
            services.AddCors();
            services.AddDbContext<AntegraContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("LaboteConnection")));

  

            services.AddIdentity<AntegraUser, UserRole>()
            .AddEntityFrameworkStores<AntegraContext>()
            .AddDefaultTokenProviders();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })




            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Enums.SecretKey))
                };
            });

            services.AddSwaggerGen(swagger =>
            {

                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Labote Api",
                    Description = "Labote laboratuar yönetim yazılımı"
                });

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter ‘Bearer’ [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {{
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                    },
                    new string[] {}}
                    });

            });



            services.AddHangfire(configuration => configuration
       .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
       .UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings().UseSqlServerStorage(Configuration.GetConnectionString("LaboteConnection"), new SqlServerStorageOptions
       {
           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
           QueuePollInterval = TimeSpan.Zero,
           UseRecommendedIsolationLevel = true,
           DisableGlobalLocks = true,

       }));

            services.AddHangfireServer();
            services.AddRazorPages();
            services.AddControllers();
  
            services.AddScoped<IHangfire, HangFire>();
       
            services.AddScoped<IAntegraContextDbSeed, AntegraContextDbSeed>();




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs,
            IWebHostEnvironment env,
            IHangfire hangfire, IAntegraContextDbSeed antegraContextDbSeed)
        {


            antegraContextDbSeed.CreateDefaultRole();
            antegraContextDbSeed.MenuSeed();
            //antegraContextDbSeed.MarketPlaceSeed(Configuration.GetSection("AntegraGetMarketPlacesUrl").Value);
      




            app.UseHangfireDashboard();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(env.ContentRootPath, "wwwroot")),
                RequestPath = "/root"
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Antegra.Api v1"));
            app.UseCors(x => x.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
                endpoints.MapHangfireDashboard();
                endpoints.MapRazorPages();
            });

        }

        //private async Task<bool>  UpdateDatabase()
        //{
        //    using (var context = new AntegraContext())
        //    {

        //            context.Database.Migrate();

        //    }
        //    return true;
        //}

    }
}
