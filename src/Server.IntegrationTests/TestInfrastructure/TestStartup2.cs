﻿using System.IO;

using BlazorHero.CleanArchitecture.Application.Extensions;
using BlazorHero.CleanArchitecture.Infrastructure.Extensions;
using BlazorHero.CleanArchitecture.Server.Controllers.Identity;
using BlazorHero.CleanArchitecture.Server.Extensions;
using BlazorHero.CleanArchitecture.Server.Filters;
using BlazorHero.CleanArchitecture.Server.Managers.Preferences;
using BlazorHero.CleanArchitecture.Server.Middlewares;

using Hangfire;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure
{
    public class TestStartup2
    {
        public TestStartup2(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {
            //services.UseEnvironment(Environment) // You can set the environment you want (development, staging, production)
            //    //.UseEnvironment("Development") // You can set the environment you want (development, staging, production)
            //    .UseConfiguration(
            //        new ConfigurationBuilder()
            //            .AddJsonFile($"appsettings.{environment}.json") //the file is set to be copied to the output directory if newer
            //            .Build()
            //    )

            services.AddCors();
            services.AddSignalR();


            services
                .AddMvc()
                .AddApplicationPart(typeof(UserController).Assembly)
                .AddApplicationPart(this.GetType().Assembly);


            services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
            services.AddCurrentUserService();
            services.AddSerialization();
            services.AddDatabase(_configuration);
            services.AddServerStorage(); //TODO - should implement ServerStorageProvider to work correctly!
            services.AddScoped<ServerPreferenceManager>();
            services.AddServerLocalization();
            services.AddIdentity();
            services.AddJwtAuthentication(services.GetApplicationSettings(_configuration));

            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = TestAuthenticationHandler.AuthScheme;
                    options.DefaultChallengeScheme = TestAuthenticationHandler.AuthScheme;
                }).AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(TestAuthenticationHandler.AuthScheme, _ => { });

            services.AddApplicationLayer();
            services.AddApplicationServices();
            services.AddRepositories();
            services.AddExtendedAttributesUnitOfWork();
            services.AddSharedInfrastructure(_configuration);
            services.RegisterSwagger();
            services.AddInfrastructureMappings();
            services.AddHangfire(x => x.UseSqlServerStorage(_configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();
            services.AddControllers().AddValidators();
            services.AddExtendedAttributesValidators();
            services.AddExtendedAttributesHandlers();
            services.AddRazorPages();
            services.AddApiVersioning(
                config =>
                {
                    config.DefaultApiVersion = new ApiVersion(1, 0);
                    config.AssumeDefaultVersionWhenUnspecified = true;
                    config.ReportApiVersions = true;
                });
            services.AddLazyCache();


            //services.AddSignalR();
            //services.AddDatabase(_configuration);

            //services
            //    .AddMvc()
            //    .AddApplicationPart(typeof(UserController).Assembly)
            //    .AddApplicationPart(this.GetType().Assembly);

            //services.AddIdentity();
            //services.AddJwtAuthentication(services.GetApplicationSettings(_configuration));

            //services.AddAuthentication(
            //    options =>
            //    {
            //        options.DefaultAuthenticateScheme = TestAuthenticationHandler.AuthScheme;
            //        options.DefaultChallengeScheme = TestAuthenticationHandler.AuthScheme;
            //    }).AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(TestAuthenticationHandler.AuthScheme, _ => { });

            //services.AddApplicationLayer();
            //services.AddApplicationServices();
            //services.AddSharedInfrastructure(_configuration);
            //services.RegisterSwagger();
            //services.AddInfrastructureMappings();
            //services.AddHangfire(x => x.UseSqlServerStorage(_configuration.GetConnectionString("DefaultConnection")));
            //services.AddHangfireServer();
            //services.AddControllers();
            //services.AddRazorPages();
            //services.AddApiVersioning(config =>
            //{
            //    config.DefaultApiVersion = new ApiVersion(1, 0);
            //    config.AssumeDefaultVersionWhenUnspecified = true;
            //    config.ReportApiVersions = true;
            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStringLocalizer<Startup> localizer)
        {

            app.UseCors();
            app.UseExceptionHandling(env);
            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseStaticFiles(
                new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
                    RequestPath = new PathString("/Files")
                });
            app.UseRequestLocalizationByCulture();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangfireDashboard(
                "/jobs",
                new DashboardOptions
                {
                    DashboardTitle = localizer["BlazorHero Jobs"],
                    Authorization = new[] { new HangfireAuthorizationFilter() }
                });
            app.UseEndpoints();
            app.ConfigureSwagger();
            app.Initialize(_configuration);

            //app.UseExceptionHandling(env);
            //app.UseHttpsRedirection();
            //app.UseHangfireDashboard("/jobs");
            //app.UseMiddleware<ErrorHandlerMiddleware>();
            //app.UseBlazorFrameworkFiles();
            //app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
            //    RequestPath = new PathString("/Files")
            //});

            //app.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();
            //app.UseEndpoints();
            //app.ConfigureSwagger();
            //app.Initialize(_configuration);

        }
    }
}