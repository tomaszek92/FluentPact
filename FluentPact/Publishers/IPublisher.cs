using FluentPact.Definitions;

namespace FluentPact.Publishers;

internal interface IPublisher
{
    Task PublishAsync(
        string consumer,
        string provider,
        PactDefinition pactDefinition,
        CancellationToken cancellationToken = default);
}
