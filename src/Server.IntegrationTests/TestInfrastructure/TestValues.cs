using System.Collections.Generic;

using BlazorHero.CleanArchitecture.Application.Enums;
using BlazorHero.CleanArchitecture.Application.Interfaces.Chat;
using BlazorHero.CleanArchitecture.Application.Models.Chat;
using BlazorHero.CleanArchitecture.Application.Requests.Identity;
using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Shared.Constants.User;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure
{
    internal static class TestValues
    {
        #region Constants

        //public const string Code = "123ABC";

        //public const string DefaultPermission = nameof(DefaultPermission);

        //public const string Id = "4734f9bf-4a08-4973-9e33-1aaf44ddc620";

        public const string Id0 = "64999b63-d952-4898-841c-c2621afd170f";

        public const string Id1 = "789aba66-2e16-40a8-81f8-4156d18959b5";

        public const string Origin = "https://example.net";

        //public const string Resource = nameof(Resource);

        public const string Route = "account/reset-password";

        //public const int SeededUserCount = 2;

        public const string Token = "TestToken";

        //public const int UserCount = 999;

        //private const string DefaultIssuer = nameof(DefaultIssuer);

        #endregion

        #region Static Fields

        public static readonly ForgotPasswordRequest ForgotPasswordRequest = new()
                                                                             {
                                                                                 Email = TestUserValues.Email
                                                                             };

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

        public static readonly ResetPasswordRequest ResetPasswordRequest = new()
                                                                           {
                                                                               ConfirmPassword = UserConstants.DefaultPassword,
                                                                               Email = TestUserValues.Email,
                                                                               Password = UserConstants.DefaultPassword,
                                                                               Token = Token
                                                                           };

        public static readonly ToggleUserStatusRequest ToggleUserStatusRequest = new()
                                                                                 {
                                                                                     ActivateUser = true,
                                                                                     UserId = Id0
                                                                                 };

        public static readonly UserRolesResponse UserRolesResponse = new()
                                                                     {
                                                                         UserRoles = new List<UserRoleModel>
                                                                                     {
                                                                                         new() { RoleName = "Basic", Selected = true, RoleDescription = "Basic role with default permissions" },
                                                                                         new() { RoleName = "Administrator", Selected = false, RoleDescription = "Administrator role with full permissions" }
                                                                                     }
                                                                     };

        public static readonly UpdateUserRolesRequest UpdateUserRolesRequest = new()
                                                                               {
                                                                                   UserId = Id0,
                                                                                   UserRoles = UserRolesResponse.UserRoles
                                                                               };

        public static readonly UserResponse UserResponse = new()
                                                           {
                                                               Email = TestUserValues.Email,
                                                               EmailConfirmed = true,
                                                               FirstName = TestUserValues.FirstName,
                                                               Id = Id0,
                                                               IsActive = true,
                                                               LastName = TestUserValues.LastName,
                                                               PhoneNumber = TestUserValues.PhoneNumber,
                                                               ProfilePictureDataUrl = string.Empty,
                                                               //ProfilePictureDataUrl = TestUserValues.ProfilePicture, //todo: transfer image data and verify it
                                                               UserName = TestUserValues.UserName
                                                           };

        #endregion

        #region Methods

        //internal static HttpClient CreateTestClient()
        //{
        //    var webHostBuilder = CreateWebHostBuilder();

        //    using var server = new TestServer(webHostBuilder);
        //    using var client= server.CreateClient();

        //    return client;
        //}

        internal static IWebHostBuilder CreateWebHostBuilder(string environment = "Development")
        {
            ////using var ctx = new TestContext();

            //////var authContext = ctx.AddTestAuthorization();
            //////authContext.SetAuthorized("TEST USER");
            //////authContext.SetRoles("admin", "superuser");
            //////authContext.SetPolicies("content-editor");
            //////authContext.SetClaims(new Claim(ClaimTypes.Email, "test@example.com"));

            var hostBuilder = new WebHostBuilder()
                .UseEnvironment(environment) // You can set the environment you want (development, staging, production)
                //.UseEnvironment("Development") // You can set the environment you want (development, staging, production)
                .UseConfiguration(
                    new ConfigurationBuilder()
                        .AddJsonFile($"appsettings.{environment}.json") //the file is set to be copied to the output directory if newer
                        .Build()
                )
                .UseStartup<TestStartup2>()
                //.ConfigureServices(s => s.AddTestUser(DefaultUser))
                .ConfigureServices(s => s.AddSingleton<IAuthenticationHandler, TestAuthenticationHandler>()) // Startup class of your web app project
                .ConfigureServices(s => s.AddHttpContextAccessor());

            //BlazorHeroContext xx;
            //xx.Database.Cr()

            return hostBuilder;
            //var host = hostBuilder.Build();
            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;

            //    try
            //    {
            //        var context = services.GetRequiredService<BlazorHeroContext>();

            //        if (context.Database.IsSqlServer())
            //        {
            //            context.Database.Migrate();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            //        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

            //        throw;
            //    }
            //}
        }

        #endregion

        public static class AccountControllerValues
        {
            #region Static Fields

            public static readonly ChangePasswordRequest ChangePasswordRequest = new()
                                                                                 {
                                                                                     ConfirmNewPassword = TestUserValues.Password,
                                                                                     NewPassword = TestUserValues.Password,
                                                                                     Password = TestUserValues.Password
                                                                                 };

            public static readonly UpdateProfilePictureRequest UpdateProfilePictureRequest = new()
                                                                                             {
                                                                                                 Data = null,
                                                                                                 Extension = "png",
                                                                                                 FileName = "TestFile",
                                                                                                 UploadType = UploadType.ProfilePicture
                                                                                             };

            public static readonly UpdateProfileRequest UpdateProfileRequest = new()
                                                                               {
                                                                                   Email = TestUserValues.Email,
                                                                                   FirstName = TestUserValues.FirstName,
                                                                                   LastName = TestUserValues.LastName,
                                                                                   PhoneNumber = TestUserValues.PhoneNumber
                                                                               };

            #endregion
        }
        
        public static class RoleClaimControllerValues
        {
            public static RoleClaimRequest CreateRoleClaimRequest(string roleId) => new()
                                                                       {
                                                                           Description = nameof(RoleClaimRequest.Description),
                                                                           Group = nameof(RoleClaimRequest.Group),
                                                                           Id = 0,
                                                                           RoleId = roleId,
                                                                           Selected = false,
                                                                           Type = "TestClaimType",
                                                                           Value = "TestClaimValue"
                                                                           
                                                                       };
        }

        public static class RoleControllerValues
        {
            //public const string RoleId = "2da7c8b9-8fa6-40eb-9fc9-ab9d64dc255c";
            public static readonly RoleRequest NewRoleRequest = new()
                                                                          {
                                                                              Description = nameof(NewRoleRequest),
                                                                              Id = null,
                                                                              Name = "TestRole"
                                                                          };
        }
        
        public static class TokenControllerValues
        {
            public static readonly TokenRequest TokenRequest = new()
                                                                {
                                                                   Password = TestUserValues.Password,
                                                                   Email = TestUserValues.Email
                                                                };

            public static readonly RefreshTokenRequest RefreshTokenRequest = new()
                                                                             {
                                                                                 Token = TestValues.Token,
                                                                                 RefreshToken = TestValues.Token
                                                                             };
        }

        public static class ChatsControllerValues
        {
            public const string ContactId = nameof(ContactId);


            public static ChatHistory<IChatUser> History = new ();

        }
        private static class TestUserValues

        {
            #region Constants

            public const string Email = "john@blazorhero.com";

            public const string FirstName = "John";

            public const string LastName = "Doe";

            public const string Password = UserConstants.DefaultPassword;

            public const string PhoneNumber = "123";

            public const string ProfilePicture = @"http://profilepicture.png";

            public const string UserName = "johndoe";

            #endregion
        }

        //public static ClaimsPrincipal CreateClaimsPrincipal(params Claim[] additionalClaims)
        //{
        //    var claims = new List<Claim>(new[] { CreateNameIdentifierClaim() });
        //    claims.AddRange(additionalClaims);

        //    return new ClaimsPrincipal(new ClaimsIdentity(claims));
        //}

        //private static Claim CreateNameIdentifierClaim(string issuer = DefaultIssuer)
        //{
        //    return new(ClaimTypes.NameIdentifier, Id, issuer);
        //}
    }

    ////public static ITestUser AdminUser = new TestUser(nameof(AdminUser), Permissions.Users.View);

    ////public static ITestUser DefaultUser = new TestUser(nameof(DefaultUser), Permissions.Users.View, Permissions.Users.Edit);

    ////public static IDictionary<string, ITestUser> TestUsers = new[] { DefaultUser, AdminUser }.ToDictionary(u => u.Name);
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

//private static IWebHostBuilder CreateWebHostBuilder(string environment = "Development")
//    => new WebHostBuilder()
//        .UseEnvironment(environment) // You can set the environment you want (development, staging, production)
//        .UseConfiguration(
//            new ConfigurationBuilder()
//                .AddJsonFile($"appsettings.{environment}.json") //the file is set to be copied to the output directory if newer
//                .Build()
//        )
//        .UseStartup<TestStartup>(); // Startup class of your web app project