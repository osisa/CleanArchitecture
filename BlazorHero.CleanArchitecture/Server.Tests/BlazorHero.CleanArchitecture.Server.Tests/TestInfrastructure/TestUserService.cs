using BlazorHero.CleanArchitecture.Application.Interfaces.Services;

namespace BlazorHero.CleanArchitecture.Server.Tests.TestInfrastructure
{
    public class TestUserService : ICurrentUserService
    {
        public string UserId { get; }
    }
}
