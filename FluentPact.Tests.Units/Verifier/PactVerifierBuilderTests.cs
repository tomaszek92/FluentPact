using System.Threading.Tasks;
using FluentPact.Verifier;
using Xunit;

namespace FluentPact.Tests.Units.Verifier;

public class PactVerifierBuilderTests
{
    [Fact]
    public async Task Should_VerifyPactFromFile()
    {
        // Arrange

        // Act
        await PactVerifierBuilder
            .Create()
            .WithConsumer("test_consumer")
            .WithProvider("test_provider")
            .RetrieveFromFile("Verifier")
            .VerifyAsync();

        // Assert
    }
}
