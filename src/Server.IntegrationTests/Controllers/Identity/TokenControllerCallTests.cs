using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;

using BlazorHero.CleanArchitecture.Application.Interfaces.Services.Identity;
using BlazorHero.CleanArchitecture.Application.Requests.Identity;
using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Infrastructure.Contexts;
using BlazorHero.CleanArchitecture.Infrastructure.Services.Identity;
using BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.TestInfrastructure;
using BlazorHero.CleanArchitecture.TestInfrastructure.TestSupport;

using Bunit;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure.TestValues;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.Controllers.Identity
{
    [TestClass]
    public class TokenControllerCallTests : TestBase
    {
        private const string BaseAddress = "api/identity/token";

        #region Public Methods and Operators

        [TestMethod]
        public void PostTokenRequest()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();

            // Act
            var result = client.Post($"{BaseAddress}", TokenControllerValues.TokenRequest);

            // Assert
            result.Should().NotBeNull();
            result.EnsureSuccessStatusCode();
            //result.Data.Count.Should().BeGreaterThan(0);
            //TestContext.WriteLine("");
        }

        //[TestMethod]
        //public void Refresh()
        //{
        //    // Arrange
        //    var webHostBuilder = CreateWebHostBuilder();

        //    using var server = new TestServer(webHostBuilder);
        //    using var client = server.CreateClient();


        //    var isx = server.Services.GetRequiredService<ITokenService>();

        //    isx.GetRefreshTokenAsync()


        //    var tokenRequest = new RefreshTokenRequest();
        //    tokenRequest.Token = GenerateJwtToken(1);
        //    tokenRequest.RefreshToken = GenerateJwtToken(1);

        //    // Act
        //    //var result = client.Post($"{BaseAddress}/refresh?Token={Token}&RefreshToken={Token}", CancellationToken.None);
        //    var result = client.Post($"{BaseAddress}/refresh", tokenRequest);

        //    // Assert
        //    result.EnsureSuccessStatusCode();
            
        //    var text = result.Content.ToResult<string>();
        //    TestContext.WriteLine("Text:{0}", text.Messages[0]);
        //}


        [TestMethod]
        public void RefreshToken()
        {
            // Arrange
            var webHostBuilder = CreateWebHostBuilder();

            using var server = new TestServer(webHostBuilder);
            using var client = server.CreateClient();
            
            var identityService = (IdentityService)server.Services.GetRequiredService<ITokenService>();
            var jwt = identityService.LoginAsync(TestValues.TokenControllerValues.TokenRequest).Result.Data;

            var token = jwt.Token;
            var refreshToken = jwt.RefreshToken;

            var tokenRequest = new RefreshTokenRequest();
            tokenRequest.Token = token;
            tokenRequest.RefreshToken = refreshToken;

            // Act
            var result = client.Post($"{BaseAddress}/refresh", tokenRequest);

            // Assert
            result.EnsureSuccessStatusCode();
            ;
        }
        

        private const string  Key= "[SECRET USED TO SIGN AND VERIFY JWT TOKENS, IT CAN BE ANY STRING]";

        public string GenerateJwtToken(int accountId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Key);
            var tokenDescriptor = new SecurityTokenDescriptor
                                  {
                                      Subject = new ClaimsIdentity(new[] { new Claim("id", accountId.ToString()) }),
                                      Expires = DateTime.UtcNow.AddDays(7),
                                      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                                  };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        [TestMethod]
        public void VerifyGenerateJwtToken()
        {
            // Arrange
            var token = GenerateJwtToken(1);

            // Act
            var result = ValidateJwtToken(token);

            // Assert
            result.Should().Be(1);
        }

        public int? ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Key);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                                                  {
                                                      ValidateIssuerSigningKey = true,
                                                      IssuerSigningKey = new SymmetricSecurityKey(key),
                                                      ValidateIssuer = false,
                                                      ValidateAudience = false,
                                                      // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                                                      ClockSkew = TimeSpan.Zero
                                                  }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return account id from JWT token if validation successful
                return accountId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        #endregion
    }
}


//var userManager = server.Services.GetRequiredService<UserManager<BlazorHeroUser>>();
//var userStore = server.Services.GetRequiredService<IUserStore<BlazorHeroUser>>();
//var user = userStore.FindByIdAsync(Id0, CancellationToken.None).Result;//client.GetAsync<Result<UserResponse>>($"/api/identity/user/{Id}").Data;
//var code = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
//var code64 = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));