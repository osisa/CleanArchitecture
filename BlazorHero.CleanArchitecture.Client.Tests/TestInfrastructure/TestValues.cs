// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="TestValues.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using Blazored.LocalStorage;

using BlazorHero.CleanArchitecture.Application.Models.Identity;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Authentication;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Identity.Account;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Identity.Authentication;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Identity.Roles;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Identity.Users;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Interceptors;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Preferences;

using Bunit;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using MudBlazor;

using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace BlazorHero.CleanArchitecture.Client.Tests.TestInfrastructure
{
    public static class TestValues
    {
        #region Public Methods and Operators

        public static TestContext GetBlazorTestContext()
        {
            using var ctx = new TestContext();
            ctx.Services.AddSingleton<IUserManager, UserManager>();
            ctx.Services.AddSingleton<IRoleManager, RoleManager>();
            ctx.Services.AddSingleton<IClientPreferenceManager, ClientPreferenceManager>();
            ctx.Services.AddBlazoredLocalStorage();
            ctx.Services.AddHttpClientInterceptor();
            ctx.Services.AddSingleton<IHttpInterceptorManager, HttpInterceptorManager>();
            ctx.Services.AddSingleton<IAuthenticationManager, AuthenticationManager>();
            ctx.Services.AddSingleton<ISnackbar, SnackbarService>();
            ctx.Services.AddSingleton<IDialogService, DialogService>();
            ctx.Services.AddSingleton<BlazorHeroStateProvider, BlazorHeroStateProvider>();
            ctx.Services.AddSingleton<IAccountManager, AccountManager>();

            return ctx;
        }

        #endregion
    }
}