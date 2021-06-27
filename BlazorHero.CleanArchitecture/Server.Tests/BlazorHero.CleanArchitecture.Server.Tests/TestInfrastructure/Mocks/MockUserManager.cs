using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BlazorHero.CleanArchitecture.Infrastructure.Models.Identity;
using BlazorHero.CleanArchitecture.TestInfrastructure;

using Bunit;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

namespace BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure.Mocks
{
    public class MockUserManager : UserManager<BlazorHeroUser>
    {
        #region Constructors and Destructors


        private readonly IList<BlazorHeroUser> _users = new List<BlazorHeroUser> { TestValues.User };

        public MockUserManager()
            : base(CreateUserStore(), CreateIdentityOptions(), CreatePasswordHasher(), CreateUserValidators(), CreatePasswordValidators(), CreateKeyNormalizer(), CreateIdentityErrorDescriber(), CreateServiceProvider(), CreateLogger())
        {
        }

        #endregion

        #region Public Methods and Operators

        public override Task<IdentityResult> CreateAsync(BlazorHeroUser user) => Task.FromResult(IdentityResult.Success);
        
        private static IUserStore<BlazorHeroUser> CreateUserStore() => TestValueFactory.CreateUserStore();

        private static IOptions<IdentityOptions> CreateIdentityOptions() => new Mock<IOptions<IdentityOptions>>().Object;

        private static IPasswordHasher<BlazorHeroUser> CreatePasswordHasher() => new Mock<IPasswordHasher<BlazorHeroUser>>().Object;

        private static IEnumerable<IUserValidator<BlazorHeroUser>> CreateUserValidators() => new List<IUserValidator<BlazorHeroUser>>(new[] { new Mock<IUserValidator<BlazorHeroUser>>().Object });

        private static IEnumerable<IPasswordValidator<BlazorHeroUser>> CreatePasswordValidators() => new List<IPasswordValidator<BlazorHeroUser>>(new[] { new Mock<IPasswordValidator<BlazorHeroUser>>().Object });
        
        private static ILookupNormalizer CreateKeyNormalizer() => new Mock<ILookupNormalizer>().Object;

        private static IdentityErrorDescriber CreateIdentityErrorDescriber() => new ();

        private static IServiceProvider CreateServiceProvider() => new Mock<IServiceProvider>().Object;

        private static ILogger<UserManager<BlazorHeroUser>> CreateLogger() => new Mock<ILogger<UserManager<BlazorHeroUser>>>().Object;

        public override IQueryable<BlazorHeroUser> Users => new TestAsyncEnumerable<BlazorHeroUser>(_users);

        #endregion
    }

    //var passwordHasher = new Mock<IPasswordHasher<BlazorHeroUser>>();
    //var userValidators = new List<IUserValidator<BlazorHeroUser>>(new[] { new Mock<IUserValidator<BlazorHeroUser>>().Object });
    //var passwordValidators = new List<IPasswordValidator<BlazorHeroUser>>(new[] { new Mock<IPasswordValidator<BlazorHeroUser>>().Object });
    //var keyNormalizer = new Mock<ILookupNormalizer>();
    //var errors = new IdentityErrorDescriber();
    //var services = new Mock<IServiceProvider>();
    //var logger = new Mock<ILogger<UserManager<BlazorHeroUser>>>();

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

}