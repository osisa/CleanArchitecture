// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="UserControllerCallTests.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.TestInfrastructure;
using BlazorHero.CleanArchitecture.TestInfrastructure.TestSupport;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.Controllers.Identity
{
    [TestClass]
    public class RoleClaimControllerCallTests : TestBase
    {
        private const string BaseAddress = "api/identity/roleClaim";

        #region Public Methods and Operators

        [TestMethod]
        public void GetAll()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.GetAsync<Result<List<RoleClaimResponse>>>($"{BaseAddress}");

            // Assert
            result.Should().NotBeNull();
            result.Data.Count.Should().BeGreaterThan(10);
            TestContext.WriteLine("");
        }
        
        [TestMethod]
        public void GetAllByRoleId()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.GetAsync<Result<List<RoleClaimResponse>>>($"{BaseAddress}/{Id0}");

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Data.Count.Should().Be(0);
        }
        
        [TestMethod]
        public void Post()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.PostAsync($"{BaseAddress}", RoleClaimControllerValues.NewRoleClaimRequest);

            // Assert
            result.EnsureSuccessStatusCode();
            
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.DeleteAsync($"{BaseAddress}/{RoleClaimControllerValues.RoleId}").Result;

            // Assert
            result.EnsureSuccessStatusCode();
        }


        #endregion
    }
}

///// <summary>
/////     Gets or sets the test context.
///// </summary>
//// ReSharper disable once UnusedMember.Global
//// ReSharper disable once MemberCanBePrivate.Global
//// ReSharper disable once UnusedAutoPropertyAccessor.Global
//public TestContext TestContext { get; set; }

//[TestMethod]
//public void RegisterAsync2()
//{
//    // Arrange
//    var startupAssembly = typeof(UserController)
//        .GetTypeInfo()
//        .Assembly;

//    var contentRoot = new FileInfo(startupAssembly.Location).Directory!.FullName;
//    var webHostBuilder = new WebHostBuilder()
//        .UseContentRoot(contentRoot)
//        .UseEnvironment("Development")
//        .UseStartup<TestStartup<UserController>>();

//   // Act
//    using (var server = new TestServer(webHostBuilder))
//    using (var client = server.CreateClient())
//    {
//        string result = client.GetStringAsync("api/identity/user/RegisterAsync").Result;

//        // Assert
//        result.Should().Contain("Chilly");
//        Messages.Count.Should().Be(1);
//    }
//}

//[TestMethod]
//public void ProbeControllerGet()
//{
//    // Arrange
//    var webHostBuilder = new WebHostBuilder()
//        .UseEnvironment("Test")
//        .UseStartup<TestStartup<ProbeController>>();

//   // Act
//    using (var server = new TestServer(webHostBuilder))
//    using (var client = server.CreateClient())
//    {
//       // Act
//        string result = client.GetStringAsync("/Probe").Result;

//        // Assert
//        result.Should().Contain("ok");
//    }
//}

//[TestMethod]
//public void ProbeControllerPing()
//{
//    // Arrange
//    var webHostBuilder = new WebHostBuilder()
//        .UseEnvironment("Test")
//        .UseStartup<TestStartup<ProbeController>>();

//   // Act
//    using (var server = new TestServer(webHostBuilder))
//    using (var client = server.CreateClient())
//    {
//       // Act
//        var  result=client.GetStringAsync("/Probe/Ping").Result;

//        // Assert
//        result.Should().Contain("PingResult");
//    }
//}

//[TestMethod]
//public void ProbeControllerPingId()
//{
//    // Arrange
//    var webHostBuilder = new WebHostBuilder()
//        .UseEnvironment("Test")
//        .UseStartup<TestStartup<ProbeController>>();

//   // Act
//    using (var server = new TestServer(webHostBuilder))
//    using (var client = server.CreateClient())
//    {
//       // Act
//        var result = client.GetStringAsync($"/Probe/PingId/{Id}").Result;

//        // Assert
//        result.Should().Contain(Id);
//    }
//}

//[TestMethod]
//public void ProbeControllerPingPushId()
//{
//    // Arrange
//    var webHostBuilder = new WebHostBuilder()
//        .UseEnvironment("Test")
//        .UseStartup<TestStartup<ProbeController>>();

//    StringContent httpContent = new StringContent("", System.Text.Encoding.UTF8, "application/json");

//   // Act
//    using (var server = new TestServer(webHostBuilder))
//    using (var client = server.CreateClient())
//    {
//       // Act
//        var result = client.PostAsync("/Probe/PingPushId", httpContent).Result;

//        // Assert
//        var text = result.Content.ReadAsStringAsync().Result;
//        text.Should().Contain("PingPushId");
//    }
//}

//[TestMethod]
//public void WeatherForecast()
//{
//    // Arrange
//    var webHostBuilder =
//        new WebHostBuilder()
//            .UseEnvironment("Test") // You can set the environment you want (development, staging, production)
//            .UseStartup<TestStartup<ProbeController>>(); // Startup class of your web app project

//    using (var server = new TestServer(webHostBuilder))
//    using (var client = server.CreateClient())
//    {
//       // Act
//        string result = client.GetStringAsync("/WeatherForecast").Result;

//        // Assert
//        result.Should().Contain("ok");
//    }
//}

//#endregion

//#region Methods

//private UserController CreateUnitUnderTest()
//{
//    Messages = new List<string>();

//    var userService = new Mock<IUserService>();

//    userService
//        .Setup(r => r.RegisterAsync(It.IsAny<RegisterRequest>(), It.IsAny<string>()))
//        .ReturnsAsync(
//            () =>
//            {
//                Messages.Add(nameof(IUserService.RegisterAsync));
//                return Result.Success();
//            });

//    //var accountService = new Mock<IAccountService>();
//    //accountService
//    //    .Setup(r => r.UpdateProfileAsync(It.IsAny<UpdateProfileRequest>(), It.IsAny<string>()))
//    //    .ReturnsAsync(
//    //        () =>
//    //        {
//    //            Messages.Add(nameof(IAccountService.UpdateProfileAsync));
//    //            return Result.Success();
//    //        });

//    return new UserController(userService.Object);
//}


//public static string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
//{
//    var projectName = startupAssembly.GetName().Name!;
//    var applicationBasePath = AppContext.BaseDirectory;
//    var directoryInfo = new DirectoryInfo(applicationBasePath);

//    do
//    {
//        directoryInfo = directoryInfo.Parent!;

//        var projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));

//        if (projectDirectoryInfo.Exists)
//            if (new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj")).Exists)
//                return Path.Combine(projectDirectoryInfo.FullName, projectName);
//    }
//    while (directoryInfo.Parent != null);

//    throw new Exception($"Project root could not be located using the application root {applicationBasePath}.");
//}

//#region Fields

//private readonly IUserService _userService;

//#endregion

//#region Constructors and Destructors

//public UserController(IUserService userService)
//{
//    _userService = userService;
//}

//#endregion

//#region Public Methods and Operators

//[HttpGet("confirm-email")]
//[AllowAnonymous]
//public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
//{
//    return Ok(await _userService.ConfirmEmailAsync(userId, code));
//}

//[HttpPost("forgot-password")]
//[AllowAnonymous]
//public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest request)
//{
//    var origin = Request.Headers["origin"];
//    return Ok(await _userService.ForgotPasswordAsync(request.Email, origin));
//}

////[Authorize(Policy = Permissions.Users.View)]
//[HttpGet]
//public async Task<IActionResult> GetAll()
//{
//    var users = await _userService.GetAllAsync();
//    return Ok(users);
//}

////[Authorize(Policy = Permissions.Users.View)]
//[HttpGet("{id}")]
//public async Task<IActionResult> GetById(string id)
//{
//    var user = await _userService.GetAsync(id);
//    return Ok(user);
//}

//[Authorize(Policy = Permissions.Users.View)]
//[HttpGet("roles/{id}")]
//public async Task<IActionResult> GetRolesAsync(string id)
//{
//    var userRoles = await _userService.GetRolesAsync(id);
//    return Ok(userRoles);
//}

//[AllowAnonymous]
//[HttpPost]
//public async Task<IActionResult> RegisterAsync(RegisterRequest request)
//{
//    var origin = Request.Headers["origin"];
//    return Ok(await _userService.RegisterAsync(request, origin));
//}

//[HttpPost("reset-password")]
//[AllowAnonymous]
//public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
//{
//    return Ok(await _userService.ResetPasswordAsync(request));
//}

//[HttpPost("toggle-status")]
//public async Task<IActionResult> ToggleUserStatusAsync(ToggleUserStatusRequest request)
//{
//    return Ok(await _userService.ToggleUserStatusAsync(request));
//}

//[Authorize(Policy = Permissions.Users.Edit)]
//[HttpPut("roles/{id}")]
//public async Task<IActionResult> UpdateRolesAsync(UpdateUserRolesRequest request)
//{
//    return Ok(await _userService.UpdateRolesAsync(request));
//}

//#endregion

//[TestMethod]
//public void Constructor()
//{
//    // Arrange

//   // Act
//    var result = CreateUnitUnderTest();

//    // Assert
//    result.Should().NotBeNull();
//    Messages.Count.Should().Be(0);
//}

//[TestMethod]
//public void PingPushRequest()
//{
//    // Arrange
//    var registerRequest = new RegisterRequest
//                          {
//                              ActivateUser = true,
//                              AutoConfirmEmail = true,
//                              ConfirmPassword = TestUser.Password,
//                              Email = TestUser.Email,
//                              FirstName = TestUser.FirstName,
//                              LastName = TestUser.LastName,
//                              Password = TestUser.Password,
//                              PhoneNumber = TestUser.PhoneNumber,
//                              UserName = TestUser.UserName
//                          };

//    var data = JsonConvert.SerializeObject(registerRequest);

//    var webHostBuilder = new WebHostBuilder()
//        .UseEnvironment("Test")
//        .UseStartup<TestStartup<ProbeController>>();

//    StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

//   // Act
//    using (var server = new TestServer(webHostBuilder))
//    using (var client = server.CreateClient())
//    {
//       // Act
//        var result = client.PostAsync("/Probe/PingPushRequest", httpContent).Result;

//        // Assert
//        var response = result.Content.ReadAsStringAsync().Result;//ReadFromJsonAsync<RegisterRequest>().Result;
//        response.Should().Be(TestUser.Email);
//    }
//}
