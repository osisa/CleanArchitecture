// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="TestAuthenticationHandler.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;

//using BlazorHero.CleanArchitecture.TestInfrastructure.Users;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure
{
    public class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        #region Constants

        /// <summary>
        ///     The name of the authorization scheme that this handler will respond to.
        /// </summary>
        public const string AuthScheme = "LocalAuth";

        public const string UserIdClaimType = nameof(UserIdClaimType);

        #endregion

        #region Static Fields

        /// <summary>
        ///     The default user id, injected into the claims for all requests.
        /// </summary>
        public static readonly Guid UserId = Guid.Parse(TestValues.Id0);// Guid.Parse("687d7c63-9a15-4faf-af5a-140782baa24d");

        #endregion

        #region Fields

        //private readonly ITestUser _testUser;

        private readonly Claim DefaultUserIdClaim = new Claim(UserIdClaimType, UserId.ToString());

        #endregion

        #region Constructors and Destructors

        //public TestAuthenticationHandler(
        //    ITestUser testUser,
        //    IOptionsMonitor<AuthenticationSchemeOptions> options,
        //    ILoggerFactory logger,
        //    UrlEncoder encoder,
        //    ISystemClock clock)
        //    : base(options, logger, encoder, clock)
        //{
        //    _testUser = testUser;
        //}

        public TestAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Marks all authentication requests as successful, and injects the
        ///     default company id into the user claims.
        /// </summary>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //var perm = new Claim("Permission", "Permissions.Users.View");

            //var authenticationTicket = new AuthenticationTicket(
            //    new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { DefaultUserIdClaim, perm }, AuthScheme)),
            //    new AuthenticationProperties(),
            //    AuthScheme);
            //return Task.FromResult(AuthenticateResult.Success(authenticationTicket));

            //var claims = new[] { new Claim(ClaimTypes.Name, "Test user"), perm };


            ////public static ITestUser DefaultUser =

            /// new TestUser(nameof(DefaultUser), Permissions.Users.View, Permissions.Users.Edit);

            ////var claims = new List<Claim> { new(ClaimTypes.Name, _testUser.Name) }; // { new Claim(ClaimTypes.Name, "Test user"), perm };
            ////claims.AddRange(_testUser.Permissions.Select(p => new Claim("Permission", p)));


            //var xx = ClaimTypes.NameIdentifier;
            //var yy = ClaimTypes.Name;

            var claims = new List<Claim> { new(ClaimTypes.Name, "DefaultUser") }; // { new Claim(ClaimTypes.Name, "Test user"), perm };
            claims.Add(new Claim(ClaimTypes.NameIdentifier, UserId.ToString()));
            claims.Add(new Claim("Permission", Permissions.Users.View)); //, Permissions.Users.Edit));
            claims.Add(new Claim("Permission", Permissions.Users.Edit)); //, Permissions.Users.Edit));

            //claims.AddRange(_testUser.Permissions.Select(p => new Claim("Permission", p)));


            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Test");

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }

        #endregion
    }
}