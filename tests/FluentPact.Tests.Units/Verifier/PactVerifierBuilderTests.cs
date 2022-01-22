using System.Threading.Tasks;
using FluentAssertions;
using FluentPact.Verifier;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace FluentPact.Tests.Units.Verifier;

public class PactVerifierBuilderTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public PactVerifierBuilderTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Should_VerifyPactFromFileWithoutErrors()
    {
        // Arrange
        var httpClient = _factory.CreateClient();

        // Act
        var result = await PactVerifierBuilder
            .Create(httpClient)
            .WithConsumer("test_consumer")
            .WithProvider("test_provider")
            .RetrieveFromFile("Verifier")
            .VerifyAsync();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
