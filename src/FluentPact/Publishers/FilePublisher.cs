using System.Text.Json;
using FluentPact.Definitions;

namespace FluentPact.Publishers;

internal class FilePublisher : IPublisher
{
    private readonly string _path;

    private readonly JsonSerializerOptions _jsonSerializerOptions =
        new() { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    public FilePublisher(string path)
    {
        _path = path;
    }

    public async Task PublishAsync(
        PactDefinition pactDefinition,
        CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_path);
        var filePath = GetPactFilePath(pactDefinition, _path);

        await using var file = File.CreateText(filePath);
        var json = JsonSerializer.Serialize(pactDefinition, _jsonSerializerOptions);
        await file.WriteAsync(json.AsMemory(), cancellationToken);
    }

    private static string GetPactFilePath(PactDefinition definition, string localPath) =>
        $"{localPath}/{definition.Provider.Name}-{definition.Consumer.Name}.json";
}
