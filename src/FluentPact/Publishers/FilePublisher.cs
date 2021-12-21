using FluentPact.Definitions;
using FluentPact.Helpers;

namespace FluentPact.Publishers;

internal class FilePublisher : IPublisher
{
    private readonly string _path;

    public FilePublisher(string path)
    {
        _path = path;
    }

    public async Task PublishAsync(
        PactDefinition pactDefinition,
        CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_path);
        var filePath = PathHelper.GetPactFilePath(pactDefinition.Provider.Name, pactDefinition.Consumer.Name, _path);

        await using var file = File.CreateText(filePath);
        var json = JsonHelper.Serialize(pactDefinition);
        await file.WriteAsync(json.AsMemory(), cancellationToken);
    }
}
