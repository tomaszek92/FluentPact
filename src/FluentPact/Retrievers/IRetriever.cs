using FluentPact.Definitions;

namespace FluentPact.Retrievers;

internal interface IRetriever
{
    Task<PactDefinition> RetrieveAsync(
        string provider, 
        string consumer,
        CancellationToken cancellationToken = default);
}
