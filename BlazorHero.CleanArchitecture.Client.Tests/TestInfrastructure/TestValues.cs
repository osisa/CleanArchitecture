// --------------------------------------------------------------------------------------------------------------------
// <copyright company="o.s.i.s.a. GmbH" file="TestValues.cs">
//    (c) 2014. See licence text in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

using Blazored.LocalStorage;

using BlazorHero.CleanArchitecture.Application.Models.Identity;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Authentication;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Audit;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Catalog.Brand;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Catalog.Product;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Communication;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Dashboard;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Document;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Identity.Account;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Identity.Authentication;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Identity.Roles;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Identity.Users;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Interceptors;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Preferences;

using Bunit;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;

using MudBlazor;
using MudBlazor.Interop;
using MudBlazor.Services;

using Newtonsoft.Json;

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

            ctx.Services.AddSingleton<IDocumentManager, DocumentManager>();
            ctx.Services.AddSingleton<IAuditManager, AuditManager>();
            ctx.Services.AddSingleton<IChatManager, ChatManager>();
            ctx.Services.AddSingleton<IDashboardManager, DashboardManager>();
            ctx.Services.AddSingleton<IBrandManager, BrandManager>();
            ctx.Services.AddSingleton<IProductManager, ProductManager>();
            ctx.Services.AddSingleton<ClientPreferenceManager, ClientPreferenceManager>();
            ctx.Services.AddSingleton<IResizeObserver, ResizeObserver>();

            ctx.Services.AddLocalization(
                options =>
                {
                    options.ResourcesPath = "Resources";
                });

            ctx.JSInterop.SetupVoid("Blazor._internal.InputFile.init", _=>true); //.SetResult("bUnit is awesome");
            ctx.JSInterop.Setup<IEnumerable<BoundingClientRect>>("mudResizeObserver.connect", _=>true).SetResult(new List<BoundingClientRect>(new BoundingClientRect[] { new BoundingClientRect(), new BoundingClientRect() }));
            
            var l = new Dictionary<string,object>();
            var x = JsonConvert.SerializeObject(l);
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(x));

            var mock = ctx.Services.AddMockHttpClient();
            ctx.JSInterop.Setup<String>("localStorage.getItem", _=>true).SetResult("Permission." + code);

            //var x = new ResizeObserver();
            //x.Unobserve()
            //ctx.JSInterop.Mode = JSRuntimeMode.Loose;

           

            return ctx;
        }

        #endregion
    }
}