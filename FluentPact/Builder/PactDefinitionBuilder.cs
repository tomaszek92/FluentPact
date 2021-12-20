using FluentPact.Definitions;
using FluentPact.Publishers;

namespace FluentPact.Builder;

public class PactDefinitionBuilder :
    IPactDefinitionBuilderOptionsStage,
    IPactDefinitionBuilderConsumerStage,
    IPactDefinitionBuilderProviderStage,
    IPactDefinitionBuilderInteractionsStage,
    IPactDefinitionBuilderPublishStage,
    IPactDefinitionBuilderFinalStage
{
    private PactDefinitionOptions? _options;
    private string? _provider;
    private string? _consumer;
    private IEnumerable<PactDefinitionInteraction>? _interactions;
    private IPublisher? _publisher;

    public IPactDefinitionBuilderConsumerStage WithOptions(PactDefinitionOptions options)
    {
        _options = options;
        return this;
    }

    public IPactDefinitionBuilderProviderStage WithConsumer(string consumer)
    {
        _consumer = consumer;
        return this;
    }

    public IPactDefinitionBuilderInteractionsStage WithProvider(string provider)
    {
        _provider = provider;
        return this;
    }

    public IPactDefinitionBuilderPublishStage WithInteractions(IEnumerable<PactDefinitionInteraction> interactions)
    {
        _interactions = interactions;
        return this;
    }

    public IPactDefinitionBuilderFinalStage PublishAsFile(string path)
    {
        _publisher = new FilePublisher(path);
        return this;
    }

    public async Task BuildAsync(CancellationToken cancellationToken = default)
    {
        if (_options is null)
        {
            throw new NullReferenceException("options");
        }

        if (_consumer is null)
        {
            throw new NullReferenceException("consumer");
        }

        if (_provider is null)
        {
            throw new NullReferenceException("provider");
        }

        if (_interactions is null)
        {
            throw new NullReferenceException("interactions");
        }

        if (_publisher is null)
        {
            throw new NullReferenceException("publisher");
        }

        PactDefinition pactDefinition = new()
        {
            Options = _options,
            Consumer = new PactDefinitionConsumer { Name = _consumer },
            Provider = new PactDefinitionProvider { Name = _provider },
            Interactions = _interactions
        };

        await _publisher.PublishAsync(_consumer, _provider, pactDefinition, cancellationToken);
    }
}
