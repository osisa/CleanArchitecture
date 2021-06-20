using BlazorHero.CleanArchitecture.Application.Interfaces.Services;

namespace BlazorHero.CleanArchitecture.Server.IntegrationTests.TestInfrastructure
{
    public class TestUserService : ICurrentUserService
    {
        #region Public Properties

        public string UserId { get; }

        #endregion
    }
}