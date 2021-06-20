// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="ServerTests.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

//using BlazorHero.CleanArchitecture.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using BlazorHero.CleanArchitecture.Application.Interfaces.Services;
using BlazorHero.CleanArchitecture.Application.Interfaces.Services.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Contexts;
using BlazorHero.CleanArchitecture.Infrastructure.Models.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Services.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Shared.Services;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests
{
    [TestClass]
    public class IdentityTests
    {
        #region Public Methods and Operators
        
        [TestMethod]
        public void UserManagerOfBlazorHerUser()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;
            
           // Act
            var result = services.GetService<UserManager<BlazorHeroUser>>();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UserManager<BlazorHeroUser>>();
        }
        
        [TestMethod]
        public void UserService()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;

           // Act
            var result = services.GetService<IUserService>();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UserService>();
        }

        [TestMethod]
        public void UserManager()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;

           // Act
            var result = services.GetService<UserManager<BlazorHeroUser>>();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UserManager<BlazorHeroUser>>();
        }

        [TestMethod]
        public void Mapper()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;
            
           // Act
            var result =services.GetService<IMapper>();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Mapper>();
        }

        [TestMethod]
        public void RoleManager()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;
            
           // Act
            var result = services.GetService<RoleManager<BlazorHeroRole>>();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<RoleManager<BlazorHeroRole>>();
        }

        [TestMethod]
        public void MailService()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;
            
           // Act
            var result = services.GetService<IMailService > ();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<SMTPMailService>();
        }

        [TestMethod]
        public void CreateUserManager()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;
            
            var userStore = services.GetRequiredService<IUserStore<BlazorHeroUser>>();
            var options = services.GetRequiredService<IOptions<IdentityOptions>>();
            var passwordHasher = services.GetRequiredService<IPasswordHasher<BlazorHeroUser>>();
            var userValidation = services.GetRequiredService<IEnumerable<IUserValidator<BlazorHeroUser>>>();
            var passwordValidator = services.GetRequiredService<IEnumerable<IPasswordValidator<BlazorHeroUser>>>();
            var lookupNormalizer = services.GetRequiredService<ILookupNormalizer>();
            var errors = services.GetRequiredService<IdentityErrorDescriber>();
            var serviceProvider = services.GetRequiredService<IServiceProvider>();
            var logger = services.GetRequiredService<ILogger<UserManager<BlazorHeroUser>>>();
            
           // Act
            var result = new UserManager<BlazorHeroUser>(userStore,options, passwordHasher, userValidation, passwordValidator, lookupNormalizer, errors, serviceProvider, logger);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UserManager<BlazorHeroUser>>();
        }

        [TestMethod]
        public void ConfigurationProvider()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;

           // Act
            var result = services.GetService<IConfigurationProvider>();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<MapperConfiguration>();
        }

        [TestMethod]
        public void CreateUserStore()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;
            var expectedType = typeof(UserStore<BlazorHeroUser, IdentityRole, BlazorHeroContext, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>);

            var context = services.GetRequiredService<BlazorHeroContext>();
            var errors = services.GetRequiredService<IdentityErrorDescriber>();
            
           // Act
            var result = new UserStore<BlazorHeroUser, IdentityRole, BlazorHeroContext, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>(context,errors);

            // Assert
            result.Should().NotBeNull();
            result.GetType().FullName.Should().Be(expectedType.FullName);
            result.Should().BeOfType<UserStore<BlazorHeroUser, IdentityRole, BlazorHeroContext, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>>();
        }

        [TestMethod]
        public void UserStore()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;
            
           // Act
            var result = services.GetRequiredService<IUserStore<BlazorHeroUser>>(); ;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UserStore<BlazorHeroUser, BlazorHeroRole, BlazorHeroContext, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityUserToken<string>, BlazorHeroRoleClaim>>();
        }
        
        [TestMethod]
        public void Options()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;


           // Act
            var result = services.GetRequiredService<IOptions<IdentityOptions>>(); ;

            // Assert
            result.Should().NotBeNull();
            result.GetType().FullName.Should().StartWith("Microsoft.Extensions.Options.OptionsManager`1[[Microsoft.AspNetCore.Identity.IdentityOptions,");
        }

        [TestMethod]
        public void PasswordHasher()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;
            
           // Act
            var result = services.GetRequiredService<IPasswordHasher<BlazorHeroUser>>(); ;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<PasswordHasher<BlazorHeroUser>>();
        }

        [TestMethod]
        public void UserValidator()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;
            
           // Act
            var result = services.GetRequiredService<IEnumerable<IUserValidator<BlazorHeroUser>>>().ToArray(); 

            // Assert
            result.Should().NotBeNull();
            result.Length.Should().Be(1);
            result[0].Should().BeOfType<UserValidator<BlazorHeroUser>>();
        }

        [TestMethod]
        public void LookupNormalizer()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;
           
           // Act
            var result = services.GetRequiredService<ILookupNormalizer>(); ;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UpperInvariantLookupNormalizer>();
        }
        
        [TestMethod]
        public void IdentityErrorDescriber()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;

           // Act
            var result = services.GetRequiredService<IdentityErrorDescriber>(); ;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<IdentityErrorDescriber>();
        }
        
        [TestMethod]
        public void ServiceProvider()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;

           // Act
            var result = services.GetRequiredService<IServiceProvider>(); ;

            // Assert
            result.Should().NotBeNull();
            result.GetType().Name.Should().Be("ServiceProviderEngineScope");
        }

        [TestMethod]
        public void Logger()
        {
            // Arrange
            var hostBuilder = CreateWebHostBuilder();
            var services = hostBuilder.Build().Services;

           // Act
            var result = services.GetRequiredService<ILogger<UserManager<BlazorHeroUser>>>(); ;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Logger<UserManager<BlazorHeroUser>>>();
        }
        
        //[TestMethod]
        //public void CreateUserService()
        //{
        //    // Arrange
        //    var host = CreateWebHostBuilder().Build();
        //    var services = host.Services;

        //    var userManager = services.GetRequiredService<UserManager<BlazorHeroUser>>();
        //    var mapper = services.GetRequiredService<IMapper>();
        //    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //    var mailService = services.GetRequiredService<IMailService>();
        //    var localizer = services.GetRequiredService<IStringLocalizer<UserService>>();

        //   // Act
        //    var result = new UserService(userManager, mapper, roleManager, mailService, localizer);

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Should().BeOfType<UserService>();
        //}
        
        #endregion
    }
}