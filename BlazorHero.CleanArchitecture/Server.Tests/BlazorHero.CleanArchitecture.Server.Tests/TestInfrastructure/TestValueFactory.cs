using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using BlazorHero.CleanArchitecture.Application.Interfaces.Services;
using BlazorHero.CleanArchitecture.Application.Interfaces.Services.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Models.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Services.Identity;
using BlazorHero.CleanArchitecture.Shared.Models;
using BlazorHero.CleanArchitecture.TestInfrastructure;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

using static BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure
{
    public static class TestValueFactory
    {
        #region Public Methods and Operators

        //public DbContext CreateDbContext()
        //{
        //    return new DbContext();
        //}

        //public IUserStore<BlazorHeroUser> CreateUserStore()
        //{
        //    var dbContext = CreateDbContext();

        //    return new UserStore<BlazorHeroUser>(dbContext);
        //}

        internal static UserManager<BlazorHeroUser> CreateBlazorHeroUserManager2()
        {
            var store = CreateUserStore();
            
            //var store = new Mock<IUserStore<BlazorHeroUser>>(); // CreateUserStore(); // 
            //var userQueryable = store.As<IQueryableUserStore<BlazorHeroUser>>();
            //var users = new[] { TestValues.User }.AsQueryable();//.AsAsyncEnumerable();
            ////var x=users.asAsyncPr

            ////var users = new[] { User };//.AsAsyncEnumerable();

            //var asyncEnumerable = new TestAsyncEnumerable<BlazorHeroUser>(users);

            //var asyncQueryProvider = new TestAsyncQueryProvider<BlazorHeroUser>(asyncEnumerable);

            ////store.Setup(i => i.Users).Returns(users);
            //userQueryable.Setup(i => i.Users).Returns(users);

            var optionsAccessor = new Mock<IOptions<IdentityOptions>>();
            var passwordHasher = new Mock<IPasswordHasher<BlazorHeroUser>>();
            var userValidators = new List<IUserValidator<BlazorHeroUser>>(new[] { new Mock<IUserValidator<BlazorHeroUser>>().Object });
            var passwordValidators = new List<IPasswordValidator<BlazorHeroUser>>(new[] { new Mock<IPasswordValidator<BlazorHeroUser>>().Object });
            var keyNormalizer = new Mock<ILookupNormalizer>();
            var errors = new IdentityErrorDescriber();
            var services = new Mock<IServiceProvider>();
            var logger = new Mock<ILogger<UserManager<BlazorHeroUser>>>();

            return new UserManager<BlazorHeroUser>(store, optionsAccessor.Object, passwordHasher.Object, userValidators, passwordValidators, keyNormalizer.Object, errors, services.Object, logger.Object);
        }

        internal static UserManager<BlazorHeroUser> CreateBlazorHeroUserManager()
        {
            var manager = new Mock<UserManager<BlazorHeroUser>>();

            //var store = new Mock<IUserStore<BlazorHeroUser>>(); // CreateUserStore(); // 
            //var userQueryable = store.As<IQueryableUserStore<BlazorHeroUser>>();
            //var users = new[] { TestValues.User }.AsQueryable();//.AsAsyncEnumerable();
            ////var x=users.asAsyncPr

            ////var users = new[] { User };//.AsAsyncEnumerable();

            //var asyncEnumerable = new TestAsyncEnumerable<BlazorHeroUser>(users);

            //var asyncQueryProvider = new TestAsyncQueryProvider<BlazorHeroUser>(asyncEnumerable);

            ////store.Setup(i => i.Users).Returns(users);
            //userQueryable.Setup(i => i.Users).Returns(users);

            //var optionsAccessor = new Mock<IOptions<IdentityOptions>>();
            //var passwordHasher = new Mock<IPasswordHasher<BlazorHeroUser>>();
            //var userValidators = new List<IUserValidator<BlazorHeroUser>>(new[] { new Mock<IUserValidator<BlazorHeroUser>>().Object });
            //var passwordValidators = new List<IPasswordValidator<BlazorHeroUser>>(new[] { new Mock<IPasswordValidator<BlazorHeroUser>>().Object });
            //var keyNormalizer = new Mock<ILookupNormalizer>();
            //var errors = new IdentityErrorDescriber();
            //var services = new Mock<IServiceProvider>();
            //var logger = new Mock<ILogger<UserManager<BlazorHeroUser>>>();

            //return new UserManager<BlazorHeroUser>(store, optionsAccessor.Object, passwordHasher.Object, userValidators, passwordValidators, keyNormalizer.Object, errors, services.Object, logger.Object);
            return manager.Object;
        }

        internal static RoleManager<BlazorHeroRole> CreateRoleManager()
        {
            var store = new Mock<IRoleStore<BlazorHeroRole>>(); // CreateUserStore(); // 
            var userQueryable = store.As<IQueryableUserStore<BlazorHeroUser>>();
            //var users = new[] { User }.AsQueryable();
            userQueryable.Setup(i => i.Users).Returns(new[] { TestValues.User }.AsQueryable());

            //var optionsAccessor = new Mock<IOptions<IdentityOptions>>();
            //var passwordHasher = new Mock<IPasswordHasher<BlazorHeroUser>>();
            var roleValidators = new List<IRoleValidator<BlazorHeroRole>>(new[] { new Mock<IRoleValidator<BlazorHeroRole>>().Object });
            //var passwordValidators = new List<IPasswordValidator<BlazorHeroUser>>(new[] { new Mock<IPasswordValidator<BlazorHeroUser>>().Object });
            var keyNormalizer = new Mock<ILookupNormalizer>();
            var errors = new IdentityErrorDescriber();
            //var services = new Mock<IServiceProvider>();
            var logger = new Mock<ILogger<RoleManager<BlazorHeroRole>>>();


            return new RoleManager<BlazorHeroRole>(store.Object, roleValidators, keyNormalizer.Object, errors, logger.Object);
        }

        internal static IUserService CreateUserService()
        {
            var userManager = new Mock<UserManager<BlazorHeroUser>>(); // CreateUserManager();//
            var mapper = new Mock<IMapper>();
            var roleManager = new Mock<RoleManager<BlazorHeroRole>>();
            var mailService = new Mock<IMailService>();
            var localizer = new Mock<IStringLocalizer<UserService>>();
            var excelService = new Mock<IExcelService>();
            var currentUserService = new Mock<ICurrentUserService>();

            return new UserService(userManager.Object, mapper.Object, roleManager.Object, mailService.Object, localizer.Object, excelService.Object, currentUserService.Object);
        }

        //internal static UserStore<BlazorHeroUser> CreateUserStore()
        //{
        //    //var userManager = CreateUserManager();//new Mock<UserManager<BlazorHeroUser>>();
        //    //var mapper = new Mock<IMapper>();
        //    //var roleManager = new Mock<RoleManager<BlazorHeroRole>>();
        //    //var mailService = new Mock<IMailService>();
        //    //var localizer = new Mock<IStringLocalizer<UserService>>();
        //    //var excelService = new Mock<IExcelService>();
        //    //var currentUserService = new Mock<ICurrentUserService>();

        //    //var dbContext = new Mock<DbContext>();
        //    //dbContext.Setup(i => i.Set<BlazorHeroUser>()).Returns(()=>new DbSet<BlazorHeroUser>());

        //    //var mock = new Mock<IDbContext>();
        //    //mock.Setup(x => x.Set<BlazorHeroUser>())
        //    //    .Returns(new FakeDbSet<BlazorHeroUser>
        //    //             {
        //    //                 new BlazorHeroUser()
        //    //             });
        //    var entities = new List<BlazorHeroUser> { TestValues.User };
        //    var dbSet = DbContextMock.GetQueryableMockDbSet(entities);
        //    var dbContextMock = new Mock<DbContext>();
        //    //dbContextMock.SetupGet(p => p.Set<BlazorHeroUser>()).Returns(() => dbSet);
        //    dbContextMock
        //        .Setup(p => p.Set<BlazorHeroUser>())
        //        .Returns(() => dbSet);

            
        //    return new UserStore<BlazorHeroUser>(dbContextMock.Object);
        //}

        internal static IUserStore<BlazorHeroUser> CreateUserStore()
        {
            //var userManager = CreateUserManager();//new Mock<UserManager<BlazorHeroUser>>();
            //var mapper = new Mock<IMapper>();
            //var roleManager = new Mock<RoleManager<BlazorHeroRole>>();
            //var mailService = new Mock<IMailService>();
            //var localizer = new Mock<IStringLocalizer<UserService>>();
            //var excelService = new Mock<IExcelService>();
            //var currentUserService = new Mock<ICurrentUserService>();

            //var dbContext = new Mock<DbContext>();
            //dbContext.Setup(i => i.Set<BlazorHeroUser>()).Returns(()=>new DbSet<BlazorHeroUser>());

            //var mock = new Mock<IDbContext>();
            //mock.Setup(x => x.Set<BlazorHeroUser>())
            //    .Returns(new FakeDbSet<BlazorHeroUser>
            //             {
            //                 new BlazorHeroUser()
            //             });
            //var entities = new List<BlazorHeroUser> { TestValues.User };
            //var dbSet = DbContextMock.GetQueryableMockDbSet(entities);
            //var dbContextMock = new Mock<DbContext>();
            ////dbContextMock.SetupGet(p => p.Set<BlazorHeroUser>()).Returns(() => dbSet);
            //dbContextMock
            //    .Setup(p => p.Set<BlazorHeroUser>())
            //    .Returns(() => dbSet);


            //return new UserStore<BlazorHeroUser>(dbContextMock.Object);

            var store = new Mock<IUserStore<BlazorHeroUser>>();
            var userQueryable = store.As<IQueryableUserStore<BlazorHeroUser>>();
            store.Setup(i => i.CreateAsync(It.IsAny<BlazorHeroUser>(), default)).ReturnsAsync(() => IdentityResult.Success);
            store.Setup(i => i.FindByIdAsync(It.IsAny<string>(), default)).ReturnsAsync(()=>TestValues.User);
            ;
            
            return userQueryable.Object;
        }

        #endregion
    }
}