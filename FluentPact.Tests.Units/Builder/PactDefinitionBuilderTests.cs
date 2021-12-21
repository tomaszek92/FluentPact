using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentPact.Builder;
using FluentPact.Definitions;
using FluentPact.Helpers;
using Xunit;

namespace FluentPact.Tests.Units.Builder;

public class PactDefinitionBuilderTests
{
    [Fact]
    public async Task Should_PublishPactDefinitionAsFile()
    {
        // Arrange
        const string outputPath = ".";

        // Act
        await PactDefinitionBuilder
            .Create()
            .WithOptions(true, true)
            .WithConsumer("test_consumer")
            .WithProvider("test_provider")
            .WithInteraction(builder => builder
                .Given("given1")
                .UponReceiving("description 1")
                .With(requestBuilder => requestBuilder
                    .WithMethod(HttpMethod.Get)
                    .WithPath("/users/8BB7865C-E9E4-4634-AF9D-CDA7EFDBFD0B"))
                .WillRespondWith(responseBuilder => responseBuilder
                    .WithStatus(HttpStatusCode.OK)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody(new { firstName = "Dave", lastName = "Pumpkin", age = 25, })))
            .WithInteraction(builder => builder
                .Given("given2")
                .UponReceiving("description 2")
                .With(requestBuilder => requestBuilder
                    .WithMethod(HttpMethod.Post)
                    .WithPath("/users")
                    .WithBody(new { firstName = "Alex", lastName = "Smash", age = 55, })
                    .WithHeader("access_token", "Bearer 4132lkhdflksayfohrqjkfhqelfig2o="))
                .WillRespondWith(responseBuilder => responseBuilder
                    .WithStatus(HttpStatusCode.Created)))
            .PublishAsFile(outputPath)
            .BuildAsync();

        // Assert
        var expected = new PactDefinition
        {
            Options = new PactDefinitionOptions { IgnoreCasing = true, IgnoreContractValues = true, },
            Consumer = new PactDefinitionConsumer { Name = "test_consumer", },
            Provider = new PactDefinitionProvider { Name = "test_provider" },
            Interactions = new List<PactDefinitionInteraction>
            {
                new()
                {
                    State = "given1",
                    Description = "description 1",
                    Request = new PactDefinitionInteractionRequest
                    {
                        Method = "GET", Path = "/users/8BB7865C-E9E4-4634-AF9D-CDA7EFDBFD0B"
                    },
                    Response = new PactDefinitionInteractionResponse
                    {
                        Status = HttpStatusCode.OK,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", "application/json" },
                        },
                        Body = new User("Dave", "Pumpkin", 25)
                    }
                },
                new()
                {
                    State = "given2",
                    Description = "description 2",
                    Request = new PactDefinitionInteractionRequest
                    {
                        Method = "POST",
                        Path = "/users",
                        Body = new User("Alex", "Smash",55),
                        Headers = new Dictionary<string, string>
                        {
                            { "access_token", "Bearer 4132lkhdflksayfohrqjkfhqelfig2o=" },
                        }
                    },
                    Response = new PactDefinitionInteractionResponse { Status = HttpStatusCode.Created }
                }
            }
        };

        var path = Path.Combine(outputPath, "test_provider-test_consumer.json");
        var pactDefinition = await JsonHelper.DeserializeAsync<PactDefinition>(path);

        foreach (var interaction in pactDefinition.Interactions)
        {
            if (interaction.Request.Body is not null)
            {
                interaction.Request.Body = JsonHelper.Deserialize<User>(interaction.Request.Body.ToString()!);
            }

            if (interaction.Response.Body is not null)
            {
                interaction.Response.Body = JsonHelper.Deserialize<User>(interaction.Response.Body.ToString()!);
            }
        }
        pactDefinition.Should().BeEquivalentTo(expected);
    }

    private class User
    {
        public User(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }
    }
}
