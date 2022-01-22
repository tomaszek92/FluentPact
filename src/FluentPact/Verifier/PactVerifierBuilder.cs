using FluentPact.Retrievers;

namespace FluentPact.Verifier;

public class PactVerifierBuilder :
    IPactVerifierBuilder.IConsumerStage,
    IPactVerifierBuilder.IProviderStage,
    IPactVerifierBuilder.IRetrieveStage,
    IPactVerifierBuilder.IFinalStage
{
    private readonly HttpClient _httpClient;
    private string? _consumer;
    private string? _provider;
    private IRetriever? _retriever;

    private PactVerifierBuilder(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public static IPactVerifierBuilder.IConsumerStage Create(HttpClient httpClient)
    {
        PactVerifierBuilder builder = new(httpClient);
        return builder;
    }

    public IPactVerifierBuilder.IProviderStage WithConsumer(string consumer)
    {
        _consumer = consumer;
        return this;
    }

    public IPactVerifierBuilder.IRetrieveStage WithProvider(string provider)
    {
        _provider = provider;
        return this;
    }

    public IPactVerifierBuilder.IFinalStage RetrieveFromFile(string path)
    {
        _retriever = new FileRetriever(path);
        return this;
    }

    public async Task VerifyAsync(CancellationToken cancellationToken = default)
    {
        if (_provider is null)
        {
            throw new FluentPactException("provider is null");
        }

        if (_consumer is null)
        {
            throw new FluentPactException("consumer is null");
        }

        if (_retriever is null)
        {
            throw new FluentPactException("retriever is null");
        }

        var pactDefinition = await _retriever.RetrieveAsync(_provider, _consumer, cancellationToken);
        var verifier = new HttpVerifier(_httpClient);
        var result = new List<PactVerifierResult>();
        await Parallel.ForEachAsync(pactDefinition.Interactions, cancellationToken, async (interaction, token) =>
        {
            var pactVerifierResult = await verifier.VerifyAsync(interaction, cancellationToken);
            result.Add(pactVerifierResult);
        });
    }
}
