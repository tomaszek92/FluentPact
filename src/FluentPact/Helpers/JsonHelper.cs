using System.Text.Json;

namespace FluentPact.Helpers;

internal static class JsonHelper
{
    private static readonly JsonSerializerOptions _options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public static string Serialize<T>(T value) =>
        JsonSerializer.Serialize(value);

    public static T Deserialize<T>(string json) =>
        JsonSerializer.Deserialize<T>(json, _options) ?? throw new Exception("Deserialized to null");

    public static async Task<T> DeserializeAsync<T>(
        string path,
        CancellationToken cancellationToken = default)
    {
        await using var stream = File.OpenRead(path);

        return await JsonSerializer.DeserializeAsync<T>(stream, _options, cancellationToken) ??
               throw new Exception("Deserialized to null");
    }
}
