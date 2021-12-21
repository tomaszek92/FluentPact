using FluentPact.Definitions;
using FluentPact.Helpers;

namespace FluentPact.Retrievers;

internal class FileRetriever : IRetriever
{
    private readonly string _path;

    public FileRetriever(string path)
    {
        _path = path;
    }

    public async Task<PactDefinition> RetrieveAsync(
        string provider,
        string consumer,
        CancellationToken cancellationToken = default)
    {
        var filePath = PathHelper.GetPactFilePath(provider, consumer, _path);
        return await JsonHelper.DeserializeAsync<PactDefinition>(filePath, cancellationToken);
    }
}
