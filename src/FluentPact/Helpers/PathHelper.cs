namespace FluentPact.Helpers;

internal static class PathHelper
{
    public static string GetPactFilePath(string provider, string consumer, string localPath) =>
        $"{localPath}/{provider}-{consumer}.json";
}
