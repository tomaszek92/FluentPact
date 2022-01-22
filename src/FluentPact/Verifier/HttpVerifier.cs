using System.Net.Http.Json;
using FluentPact.Definitions;

namespace FluentPact.Verifier;

internal class HttpVerifier
{
    private readonly HttpClient _httpClient;

    public HttpVerifier(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PactVerifierInteractionResult> VerifyAsync(
        PactDefinitionInteraction interaction,
        CancellationToken cancellationToken = default)
    {
        var result = new PactVerifierInteractionResult();
        using var httpRequestMessage = GetHttpRequestMessage(interaction.Request);
        var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage, cancellationToken);
        VerifyStatusCode(interaction.Response, httpResponseMessage, result);
        VerifyHeaders(interaction.Response, httpResponseMessage, result);
        await VerifyBodyAsync(interaction.Response, httpResponseMessage, result, cancellationToken);

        return result;
    }

    private static HttpRequestMessage GetHttpRequestMessage(PactDefinitionInteractionRequest request)
    {
        var httpMethod = GetHttpMethod(request.Method);
        var message = new HttpRequestMessage(httpMethod, request.Path);

        foreach (var (key, value) in request.Headers)
        {
            message.Headers.Add(key, value);
        }

        if (request.Body is not null)
        {
            message.Content = JsonContent.Create(request.Body);
        }

        return message;
    }

    private static HttpMethod GetHttpMethod(string httpMethod)
    {
        return httpMethod switch
        {
            "GET" => HttpMethod.Get,
            "POST" => HttpMethod.Post,
            "PUT" => HttpMethod.Put,
            "DELETE" => HttpMethod.Delete,
            _ => throw new FluentPactException($"Unsupported HTTP method: {httpMethod}"),
        };
    }

    private static void VerifyStatusCode(
        PactDefinitionInteractionResponse interactionResponse,
        HttpResponseMessage httpResponseMessage,
        PactVerifierInteractionResult result)
    {
        if (httpResponseMessage.StatusCode != interactionResponse.Status)
        {
            result.AddError(
                $"Expected response status code: {interactionResponse.Status}, actual: {httpResponseMessage.StatusCode}");
        }
    }

    private static void VerifyHeaders(
        PactDefinitionInteractionResponse interactionResponse,
        HttpResponseMessage httpResponseMessage,
        PactVerifierInteractionResult result)
    {
        foreach (var (expectedKey, expectedValues) in interactionResponse.Headers)
        {
            if (!httpResponseMessage.Headers.TryGetValues(expectedKey, out var values))
            {
                result.AddError($"Missing expected header: ${expectedKey}");
            }
            else if (!values.SequenceEqual(expectedValues))
            {
                result.AddError(
                    $"Expected header (${expectedKey}) values: {string.Join(", ", expectedKey)}, actual: {string.Join(", ", values)}");
            }
        }
    }

    private static async Task VerifyBodyAsync(
        PactDefinitionInteractionResponse interactionResponse,
        HttpResponseMessage httpResponseMessage,
        PactVerifierInteractionResult result,
        CancellationToken cancellationToken = default)
    {
        if (interactionResponse.Body is null)
        {
            return;
        }

        // var json = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        // var providedBody = await JsonHelper.DeserializeAsync<ExpandoObject>(json, cancellationToken);
        // var expectedBody = interactionResponse.Body;
    }
}
