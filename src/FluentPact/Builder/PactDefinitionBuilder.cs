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
    private IPublisher? _publisher;
    private readonly PactDefinition _pactDefinition = new();
    
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
        _pactDefinition.Options = new PactDefinitionOptions
        {
            IgnoreCasing = ignoreCasing, IgnoreContractValues = ignoreContractValues
        };
        return this;
    }

    public IPactDefinitionBuilderProviderStage WithConsumer(string consumer)
    {
        _pactDefinition.Consumer = new PactDefinitionConsumer { Name = consumer };
        return this;
    }

    public IPactDefinitionBuilderInteractionsStage WithProvider(string provider)
    {
        _pactDefinition.Provider = new PactDefinitionProvider { Name = provider };
        return this;
    }

    public IPactDefinitionBuilderInteractionsStage WithInteraction(Action<IPactDefinitionInteractionBuilder> action)
    {
        var builder = new PactDefinitionInteractionBuilder();
        action(builder);
        var interaction = builder.Build();
        _pactDefinition.Interactions.Add(interaction);
        return this;
    }

    public IPactDefinitionBuilderFinalStage PublishAsFile(string path)
    {
        _publisher = new FilePublisher(path);
        return this;
    }

    public async Task BuildAsync(CancellationToken cancellationToken = default)
    {
        if (_publisher is null)
        {
            throw new NullReferenceException("provider");
        }
        
        await _publisher.PublishAsync(_pactDefinition, cancellationToken);
    }
}
