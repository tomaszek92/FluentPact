using FluentPact.Builder.Interaction;
using FluentPact.Definitions;
using FluentPact.Publishers;

namespace FluentPact.Builder;

public class PactDefinitionBuilder :
    IPactDefinitionBuilderOptionsStage,
    IPactDefinitionBuilderConsumerStage,
    IPactDefinitionBuilderProviderStage,
    IPactDefinitionBuilderInteractionsStage,
    IPactDefinitionBuilderFinalStage
{
    private PactDefinitionOptions? _options;
    private string? _provider;
    private string? _consumer;
    private List<PactDefinitionInteraction> _interactions = new();
    private IPublisher? _publisher;

    private PactDefinitionBuilder()
    {
    }

    public static IPactDefinitionBuilderOptionsStage Create()
    {
        PactDefinitionBuilder builder = new();
        return builder;
    }
    
    public IPactDefinitionBuilderConsumerStage WithOptions(bool ignoreCasing, bool ignoreContractValues)
    {
        _options = new PactDefinitionOptions
        {
            IgnoreCasing = ignoreCasing, IgnoreContractValues = ignoreContractValues
        };
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

    public IPactDefinitionBuilderInteractionsStage WithInteraction(Action<IPactDefinitionInteractionBuilder> action)
    {
        var builder = new PactDefinitionInteractionBuilder();
        action(builder);
        var interaction = builder.Build();
        _interactions.Add(interaction);
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

        if (!_interactions.Any())
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
