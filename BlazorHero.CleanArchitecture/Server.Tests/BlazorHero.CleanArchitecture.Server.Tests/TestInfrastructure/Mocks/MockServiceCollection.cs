using System;
using System.Collections.Generic;

using BlazorHero.CleanArchitecture.Infrastructure.Models.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

namespace BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure.Mocks
{
    public static class MockServiceProvider //: IServiceProvider
    {
        #region Fields

        //private readonly IServiceProvider _inner;

        #endregion

        #region Constructors and Destructors

        //public MockServiceProvider()
        //{
        //    //var serviceCollection = new ServiceCollection();

        //    //serviceCollection.AddSingleton(CreateIdentityErrorDescriber());
        //    //serviceCollection.AddSingleton(CreateIdentityOptions());
        //    //serviceCollection.AddSingleton(CreateKeyNormalizer());
        //    //serviceCollection.AddSingleton(CreateLogger());
        //    //serviceCollection.AddSingleton(CreatePasswordHasher());
        //    //serviceCollection.AddSingleton(CreatePasswordValidators());
        //    //serviceCollection.AddSingleton(CreateServiceProvider());
        //    //serviceCollection.AddSingleton(CreateUserStore());
        //    //serviceCollection.AddSingleton(CreateUserValidators());

        //    //_inner = serviceCollection.BuildServiceProvider();
        //}

        #endregion

        #region Public Methods and Operators

        //public object? GetService(Type serviceType) => _inner.GetService(serviceType);

        //public static void Setup()
        //{
        //    Setup(new ServiceCollection());
        //}

        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(CreateIdentityErrorDescriber());
            serviceCollection.AddSingleton(CreateIdentityOptions());
            serviceCollection.AddSingleton(CreateKeyNormalizer());
            serviceCollection.AddSingleton(CreateLogger());
            serviceCollection.AddSingleton(CreatePasswordHasher());
            serviceCollection.AddSingleton(CreatePasswordValidators());
            serviceCollection.AddSingleton(CreateServiceProvider());
            serviceCollection.AddSingleton(CreateUserStore());
            serviceCollection.AddSingleton(CreateUserValidators());

        }

        #endregion

        #region Methods

        private static IdentityErrorDescriber CreateIdentityErrorDescriber() => new();

        private static IOptions<IdentityOptions> CreateIdentityOptions() => new Mock<IOptions<IdentityOptions>>().Object;

        private static ILookupNormalizer CreateKeyNormalizer() => new Mock<ILookupNormalizer>().Object;

        private static ILogger<UserManager<BlazorHeroUser>> CreateLogger() => new Mock<ILogger<UserManager<BlazorHeroUser>>>().Object;

        private static IPasswordHasher<BlazorHeroUser> CreatePasswordHasher() => new Mock<IPasswordHasher<BlazorHeroUser>>().Object;

        private static IEnumerable<IPasswordValidator<BlazorHeroUser>> CreatePasswordValidators() => new List<IPasswordValidator<BlazorHeroUser>>(new[] { new Mock<IPasswordValidator<BlazorHeroUser>>().Object });

        private static IServiceProvider CreateServiceProvider() => new Mock<IServiceProvider>().Object;

        private static IUserStore<BlazorHeroUser> CreateUserStore() => TestValueFactory.CreateUserStore();

        private static IEnumerable<IUserValidator<BlazorHeroUser>> CreateUserValidators() => new List<IUserValidator<BlazorHeroUser>>(new[] { new Mock<IUserValidator<BlazorHeroUser>>().Object });

        #endregion
    }
}