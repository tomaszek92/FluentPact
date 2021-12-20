using FluentPact.Definitions;

namespace FluentPact.Publishers;

internal interface IPublisher
{
    Task PublishAsync(
        PactDefinition pactDefinition,
        CancellationToken cancellationToken = default);
}
