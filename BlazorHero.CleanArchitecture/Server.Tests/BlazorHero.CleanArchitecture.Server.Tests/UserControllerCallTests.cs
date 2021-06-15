// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="UserControllerCallTests.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

using BlazorHero.CleanArchitecture.Application.Models.Identity;
using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.TestInfrastructure;

using FluentAssertions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using static BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.Tests
{
    [TestClass]
    public class UserControllerCallTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void GetAll()
        {
            // arrange
            var webHostBuilder = CreateWebHostBuilder();

            //new WebHostBuilder()
            //    .UseEnvironment("Test") // You can set the environment you want (development, staging, production)
            //    //.UseEnvironment("Development") // You can set the environment you want (development, staging, production)
            //    .UseConfiguration(
            //        new ConfigurationBuilder()
            //            .AddJsonFile("appsettings.Development.json") //the file is set to be copied to the output directory if newer
            //            .Build()
            //    )
            //    .UseStartup<TestStartup>(); // Startup class of your web app project

            // act
            using (var server = new TestServer(webHostBuilder))
            {
                using (var client = server.CreateClient())
                {
                    var result = client.GetAsync<Result<List<UserResponse>>>("/api/identity/user/getall");

                    result.Succeeded.Should().BeTrue();

                    result.Data.Should().NotBeNull();
                    result.Data.Count.Should().Be(2);
                    result.Data[0].Should().BeEquivalentTo(TestValues.UserResponse);
                }
            }
        }

        [TestMethod]
        public void GetById()
        {
            // arrange
            var webHostBuilder = CreateWebHostBuilder();

            // act
            using (var server = new TestServer(webHostBuilder))

            using (var client = server.CreateClient())
            {
                // act
                var result = client.GetAsync<Result<UserResponse>>($"/api/identity/user/{Id}");

                // assert
                result.Succeeded.Should().BeTrue();
                result.Data.Should().BeEquivalentTo(TestValues.UserResponse);
            }
        }

        //[TestMethod]
        //public void ConfirmEmailAsync()
        //{
        //    // arrange
        //    var webHostBuilder = CreateWebHostBuilder();

        //    // act
        //    using (var server = new TestServer(webHostBuilder))
        //    {
        //        using (var client = server.CreateClient())
        //        {
        //            // act
        //            var result = client.GetAsync($"/api/identity/user/confirm-email?userId={Id}&code={Code}").Result;

        //            // assert
        //            result.EnsureSuccessStatusCode();
        //        }
        //    }
        //}


        [TestMethod]
        public void ConfirmEmailAsync()
        {
            // arrange
            var webHostBuilder = CreateWebHostBuilder();

            // act
            using (var server = new TestServer(webHostBuilder))
            {
                using (var client = server.CreateClient())
                {
                    // act
                    
                    var userManager = server.Services.GetRequiredService<UserManager<BlazorHeroUser>>();
                    var userStore = server.Services.GetRequiredService<IUserStore<BlazorHeroUser>>();
                    var user = userStore.FindByIdAsync(Id, CancellationToken.None).Result;//client.GetAsync<Result<UserResponse>>($"/api/identity/user/{Id}").Data;
                    var code = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                    var code64 = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    // act
                    var result = client.GetAsync($"/api/identity/user/confirm-email?userId={Id}&code={code64}").Result;

                    // assert
                    result.EnsureSuccessStatusCode();
                }
            }
        }

        [TestMethod]
        public void ForgotPasswordAsync()
        {
            // arrange
            var webHostBuilder = CreateWebHostBuilder();

            // act
            using (var server = new TestServer(webHostBuilder))
            {
                using (var client = server.CreateClient())
                {
                    var req = new HttpRequestMessage(HttpMethod.Post, "/api/identity/user/forgot-password");
                    var data = JsonConvert.SerializeObject(ForgotPasswordRequest);

                    StringContent httpContent = new(data, System.Text.Encoding.UTF8, "application/json");
                    req.Content = httpContent;
                    req.Headers.Add("origin",Origin);

                    // act
                    //var result = client.PostAsync("/api/identity/user/forgot-password", ForgotPasswordRequest);
                    var result = client.SendAsync(req).Result;

                    // assert
                    result.EnsureSuccessStatusCode();
                }
            }
        }

        [TestMethod]
        public void CheckOrigin()
        {
            // arrange
            var origin = Origin;
            var route = Route;

            // act
            var result =new Uri(string.Concat($"{origin}/", route));

            // assert
            result.Should().NotBeNull();
        }

        //[TestMethod]
        //public void ForgotPasswordAsyn2()
        //{
        //    // arrange
        //    var webHostBuilder = CreateWebHostBuilder();

        //    // act
        //    using (var server = new TestServer(webHostBuilder))
        //    {


        //        using (var client = server.CreateClient())
        //        {
        //            // act
        //            var result = client.PostAsync("/api/identity/user/forgot-password", ForgotPasswordRequest);

        //            // assert
        //            result.EnsureSuccessStatusCode();
        //        }
        //    }
        //}

        //[TestMethod]
        //public void GetRolesAsync()
        //{
        //    // arrange
        //    var webHostBuilder = CreateWebHostBuilder();

        //    // act
        //    using (var server = new TestServer(webHostBuilder))
        //    using (var client = server.CreateClient())
        //    {
        //        // act
        //        var result = client.GetAsync<Result<UserRolesResponse>>($"/api/identity/user/roles/{Id}");

        //        // assert
        //        result.Succeeded.Should().BeTrue();
        //        result.Data.UserRoles.Should().BeEquivalentTo(TestValues.UserRolesResponse.UserRoles);
        //    }
        //}

        [TestMethod]
        public void GetRolesForUserIdAsync()
        {
            // arrange
            var webHostBuilder = CreateWebHostBuilder();

            // act
            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                // act
                var result = client.GetAsync<Result<UserRolesResponse>>($"/api/identity/user/roles/{Id}");

                // assert
                result.Succeeded.Should().BeTrue();
                result.Data.UserRoles.Should().BeEquivalentTo(TestValues.UserRolesResponse.UserRoles);
            }
        }

        [TestMethod]
        public void RegisterAsync()
        {
            // arrange
            var webHostBuilder = CreateWebHostBuilder();

            // act
            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                // act
                //client.
                var result = client.PostAsync("/api/identity/user/register", RegisterRequest);

                // assert
                result.EnsureSuccessStatusCode();
            }
        }

        [TestMethod]
        public void ResetPasswordAsync()
        {
            // arrange
            var webHostBuilder = CreateWebHostBuilder();

            // act
            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                // act
                var result = client.PostAsync("/api/identity/user/reset-password", ResetPasswordRequest);

                // assert
                result.EnsureSuccessStatusCode();
            }
        }


        [TestMethod]
        public void GetAuthenticationHandler()
        {
            // arrange
            var webHostBuilder = CreateWebHostBuilder();
            var host = webHostBuilder.Build();

            // act
            var result = host.Services.GetService<IAuthenticationHandler>();

            // assert
            result.Should().NotBeNull();
        }


        [TestMethod]
        public void ToggleUserStatusAsync()
        {
            // arrange
            var webHostBuilder = CreateWebHostBuilder();

            // act
            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                // act
                var result = client.PostAsync("/api/identity/user/toggle-status", TestValues.ToggleUserStatusRequest);

                // assert
                result.EnsureSuccessStatusCode();
            }
        }

        [TestMethod]
        public void UpdateRolesAsync()
        {
            // arrange
            var webHostBuilder = CreateWebHostBuilder();

            // act
            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                // act
                var result = client.PutAsync($"/api/identity/user/roles/{Id}", TestValues.UpdateUserRolesRequest);

                // assert
                result.EnsureSuccessStatusCode();
            }
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
//    // arrange
//    var startupAssembly = typeof(UserController)
//        .GetTypeInfo()
//        .Assembly;

//    var contentRoot = new FileInfo(startupAssembly.Location).Directory!.FullName;
//    var webHostBuilder = new WebHostBuilder()
//        .UseContentRoot(contentRoot)
//        .UseEnvironment("Development")
//        .UseStartup<TestStartup<UserController>>();

//    // act
//    using (var server = new TestServer(webHostBuilder))
//    using (var client = server.CreateClient())
//    {
//        string result = client.GetStringAsync("api/identity/user/RegisterAsync").Result;

//        // assert
//        result.Should().Contain("Chilly");
//        Messages.Count.Should().Be(1);
//    }
//}

//[TestMethod]
//public void ProbeControllerGet()
//{
//    // arrange
//    var webHostBuilder = new WebHostBuilder()
//        .UseEnvironment("Test")
//        .UseStartup<TestStartup<ProbeController>>();

//    // act
//    using (var server = new TestServer(webHostBuilder))
//    using (var client = server.CreateClient())
//    {
//        // act
//        string result = client.GetStringAsync("/Probe").Result;

//        // assert
//        result.Should().Contain("ok");
//    }
//}

//[TestMethod]
//public void ProbeControllerPing()
//{
//    // arrange
//    var webHostBuilder = new WebHostBuilder()
//        .UseEnvironment("Test")
//        .UseStartup<TestStartup<ProbeController>>();

//    // act
//    using (var server = new TestServer(webHostBuilder))
//    using (var client = server.CreateClient())
//    {
//        // act
//        var  result=client.GetStringAsync("/Probe/Ping").Result;

//        // assert
//        result.Should().Contain("PingResult");
//    }
//}

//[TestMethod]
//public void ProbeControllerPingId()
//{
//    // arrange
//    var webHostBuilder = new WebHostBuilder()
//        .UseEnvironment("Test")
//        .UseStartup<TestStartup<ProbeController>>();

//    // act
//    using (var server = new TestServer(webHostBuilder))
//    using (var client = server.CreateClient())
//    {
//        // act
//        var result = client.GetStringAsync($"/Probe/PingId/{Id}").Result;

//        // assert
//        result.Should().Contain(Id);
//    }
//}

//[TestMethod]
//public void ProbeControllerPingPushId()
//{
//    // arrange
//    var webHostBuilder = new WebHostBuilder()
//        .UseEnvironment("Test")
//        .UseStartup<TestStartup<ProbeController>>();

//    StringContent httpContent = new StringContent("", System.Text.Encoding.UTF8, "application/json");

//    // act
//    using (var server = new TestServer(webHostBuilder))
//    using (var client = server.CreateClient())
//    {
//        // act
//        var result = client.PostAsync("/Probe/PingPushId", httpContent).Result;

//        // assert
//        var text = result.Content.ReadAsStringAsync().Result;
//        text.Should().Contain("PingPushId");
//    }
//}

//[TestMethod]
//public void WeatherForecast()
//{
//    // arrange
//    var webHostBuilder =
//        new WebHostBuilder()
//            .UseEnvironment("Test") // You can set the environment you want (development, staging, production)
//            .UseStartup<TestStartup<ProbeController>>(); // Startup class of your web app project

//    using (var server = new TestServer(webHostBuilder))
//    using (var client = server.CreateClient())
//    {
//        // act
//        string result = client.GetStringAsync("/WeatherForecast").Result;

//        // assert
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
//    // arrange

//    // act
//    var result = CreateUnitUnderTest();

//    // assert
//    result.Should().NotBeNull();
//    Messages.Count.Should().Be(0);
//}

//[TestMethod]
//public void PingPushRequest()
//{
//    // arrange
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

//    // act
//    using (var server = new TestServer(webHostBuilder))
//    using (var client = server.CreateClient())
//    {
//        // act
//        var result = client.PostAsync("/Probe/PingPushRequest", httpContent).Result;

//        // assert
//        var response = result.Content.ReadAsStringAsync().Result;//ReadFromJsonAsync<RegisterRequest>().Result;
//        response.Should().Be(TestUser.Email);
//    }
//}
