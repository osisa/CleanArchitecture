using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using BlazorHero.CleanArchitecture.Application.Requests.Identity;
using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using BlazorHero.CleanArchitecture.TestInfrastructure;
using BlazorHero.CleanArchitecture.TestInfrastructure.Extensions;
using BlazorHero.CleanArchitecture.TestInfrastructure.Users;

using Bunit;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure
{
    internal static class TestValues
    {
        #region Constants

        public const string Code = "123ABC";

        public const string DefaultPermission = nameof(DefaultPermission);

        public const string Id = "4734f9bf-4a08-4973-9e33-1aaf44ddc620";

        public const string Id0 = "64999b63-d952-4898-841c-c2621afd170f";
        public const string Id1 = "789aba66-2e16-40a8-81f8-4156d18959b5";

        public const string Origin = "https://example.net";

        public const string Resource = nameof(Resource);

        public const string Route = "account/reset-password";

        public const int SeededUserCount = 2;

        public const string Token = "TestToken";

        public const int UserCount = 999;

        #endregion

        #region Static Fields

        public static readonly RegisterRequest RegisterRequest = new()
                                                                 {
                                                                     ActivateUser = true,
                                                                     AutoConfirmEmail = true,
                                                                     ConfirmPassword = TestUserValues.Password,
                                                                     Email = TestUserValues.Email,
                                                                     FirstName = TestUserValues.FirstName,
                                                                     LastName = TestUserValues.LastName,
                                                                     Password = TestUserValues.Password,
                                                                     PhoneNumber = TestUserValues.PhoneNumber,
                                                                     UserName = TestUserValues.UserName
                                                                 };

        public static readonly UserResponse UserResponse = new()
                                                           {
                                                               Email = TestUserValues.Email,
                                                               EmailConfirmed = true,
                                                               FirstName = TestUserValues.FirstName,
                                                               Id = Id0,
                                                               IsActive = true,
                                                               LastName = TestUserValues.LastName,
                                                               PhoneNumber = null,
                                                               ProfilePictureDataUrl = null,
                                                               UserName = TestUserValues.UserName
                                                           };

        public static readonly UserRolesResponse UserRolesResponse = new()
                                                                     {
                                                                         UserRoles = new List<UserRoleModel>
                                                                                     {
                                                                                         new() { RoleName = "Basic", Selected = true, RoleDescription = "Basic role with default permissions"},
                                                                                         new() { RoleName = "Administrator", Selected = false, RoleDescription = "Administrator role with full permissions" }
                                                                                     }
                                                                     };

        public static readonly ResetPasswordRequest ResetPasswordRequest = new()
                                                                           {
                                                                               Email = TestUserValues.Email,
                                                                               Password = BlazorHero.CleanArchitecture.Shared.Constants.User.UserConstants.DefaultPassword,
                                                                               Token = Token
                                                                           };

        public static readonly ToggleUserStatusRequest ToggleUserStatusRequest = new()
                                                                                 {
                                                                                     ActivateUser = true,
                                                                                     UserId = Id0
                                                                                 };

        public static readonly UpdateUserRolesRequest UpdateUserRolesRequest = new()
                                                                               {
                                                                                   UserId = Id0,
                                                                                   UserRoles = UserRolesResponse.UserRoles
                                                                               };

        public static readonly ForgotPasswordRequest ForgotPasswordRequest = new()
                                                                             {
                                                                                 Email = TestUserValues.Email
                                                                             };

        public static ITestUser AdminUser = new TestUser(nameof(AdminUser), Permissions.Users.View);

        public static ITestUser DefaultUser = new TestUser(nameof(DefaultUser), Permissions.Users.View, Permissions.Users.Edit);

        public static IDictionary<string, ITestUser> TestUsers = new[] { DefaultUser, AdminUser }.ToDictionary(u => u.Name);

        #endregion

        #region Public Methods and Operators

        public static ClaimsPrincipal CreateClaimsPrincipal(params Claim[] additionalClaims)
        {
            var claims = new List<Claim>(new[] { CreateNameIdentifierClaim() });
            claims.AddRange(additionalClaims);

            return new ClaimsPrincipal(new ClaimsIdentity(claims));
        }

        #endregion

        #region Methods

        internal static IWebHostBuilder CreateWebHostBuilder(string environment = Environments.Development)
        {
            using var ctx = new TestContext();

            //var authContext = ctx.AddTestAuthorization();
            //authContext.SetAuthorized("TEST USER");
            //authContext.SetRoles("admin", "superuser");
            //authContext.SetPolicies("content-editor");
            //authContext.SetClaims(new Claim(ClaimTypes.Email, "test@example.com"));

            return new WebHostBuilder()
                .UseEnvironment(environment) // You can set the environment you want (development, staging, production)
                //.UseEnvironment("Development") // You can set the environment you want (development, staging, production)
                .UseConfiguration(
                    new ConfigurationBuilder()
                        .AddJsonFile($"appsettings.{environment}.json") //the file is set to be copied to the output directory if newer
                        .Build()
                )
                .UseStartup<TestStartup>()
                .ConfigureServices(s => s.AddTestUser(DefaultUser))
                .ConfigureServices(s => s.AddSingleton<IAuthenticationHandler, TestAuthenticationHandler>()) // Startup class of your web app project
                .ConfigureServices(s => s.AddHttpContextAccessor());
        }

        private static Claim CreateNameIdentifierClaim(string issuer = DefaultIssuer)
        {
            return new (ClaimTypes.NameIdentifier, Id, issuer);
        }

        private const string DefaultIssuer = nameof(DefaultIssuer);

        #endregion

        private static class TestUserValues
        {
            #region Constants

            public const string Email = "john@blazorhero.com";

            public const string FirstName = "John";

            public const string LastName = "Doe";

            public const string Password = BlazorHero.CleanArchitecture.Shared.Constants.User.UserConstants.DefaultPassword;

            public const string PhoneNumber = "123";

            public const string ProfilePicture = @"http://profilepicture.png";

            public const string UserName = "johndoe";

            #endregion
        }
    }
}

//public static TestContext GetBlazorTestContext()
//{
//    using var ctx = new TestContext();
//    ctx.Services.AddSingleton<IUserManager, UserManager>();
//    ctx.Services.AddSingleton<IRoleManager, RoleManager>();
//    ctx.Services.AddSingleton<IClientPreferenceManager, ClientPreferenceManager>();
//    ctx.Services.AddBlazoredLocalStorage();
//    ctx.Services.AddHttpClientInterceptor();
//    ctx.Services.AddSingleton<IHttpInterceptorManager, HttpInterceptorManager>();
//    ctx.Services.AddSingleton<IAuthenticationManager, AuthenticationManager>();
//    ctx.Services.AddSingleton<ISnackbar, SnackbarService>();
//    ctx.Services.AddSingleton<IDialogService, DialogService>();
//    ctx.Services.AddSingleton<BlazorHeroStateProvider, BlazorHeroStateProvider>();
//    ctx.Services.AddSingleton<IAccountManager, AccountManager>();

//    return ctx;
//}

//public static IUserService GetMockUserService()
//    => new MockUserService();

//private class MockUserService : IUserService
//{
//    #region Public Methods and Operators

//    public async Task<IResult<string>> ConfirmEmailAsync(string userId, string code)
//        => await Result<string>.SuccessAsync(Id, message: nameof(IUserService.ConfirmEmailAsync));

//    public async Task<IResult> ForgotPasswordAsync(string emailId, string origin)
//        => await Result<string>.SuccessAsync(nameof(IUserService.ForgotPasswordAsync));

//    public async Task<Result<List<UserResponse>>> GetAllAsync()
//        => await Result<List<UserResponse>>.SuccessAsync(new List<UserResponse> { UserResponse });

//    public async Task<IResult<UserResponse>> GetAsync(string userId)
//        => await Result<UserResponse>.SuccessAsync(UserResponse);

//    public Task<int> GetCountAsync()
//        => Task.FromResult(UserCount);

//    public async Task<IResult<UserRolesResponse>> GetRolesAsync(string id)
//        => await Result<UserRolesResponse>.SuccessAsync(UserRolesResponse);

//    public async Task<IResult> RegisterAsync(RegisterRequest request, string origin)
//        => await Result<RegisterRequest>.SuccessAsync(RegisterRequest);

//    public async Task<IResult> ResetPasswordAsync(ResetPasswordRequest request)
//        => await Result<ResetPasswordRequest>.SuccessAsync(ResetPasswordRequest);

//    public async Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request)
//        => await Result<ToggleUserStatusRequest>.SuccessAsync(ToggleUserStatusRequest);

//    public async Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request)
//        => await Result<UpdateUserRolesRequest>.SuccessAsync(UpdateUserRolesRequest);

//    #endregion
//}