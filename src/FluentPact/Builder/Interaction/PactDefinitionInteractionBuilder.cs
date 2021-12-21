using FluentPact.Builder.Interaction.Request;
using FluentPact.Builder.Interaction.Response;
using FluentPact.Definitions;

namespace FluentPact.Builder.Interaction;

internal class PactDefinitionInteractionBuilder :
    IPactDefinitionInteractionBuilder.IGivenStage,
    IPactDefinitionInteractionBuilder.IUponReceivingStage,
    IPactDefinitionInteractionBuilder.IWithStage,
    IPactDefinitionInteractionBuilder.IWillRespondWithStage
{
    private readonly PactDefinitionInteraction _interaction = new();

    public IPactDefinitionInteractionBuilder.IUponReceivingStage Given(string state)
    {
        _interaction.State = state;
        return this;
    }

    public IPactDefinitionInteractionBuilder.IWithStage UponReceiving(string description)
    {
        _interaction.Description = description;
        return this;
    }

    public IPactDefinitionInteractionBuilder.IWillRespondWithStage With(Action<IPactDefinitionInteractionRequestBuilder> action)
    {
        var builder = new PactDefinitionInteractionRequestBuilder();
        action(builder);
        _interaction.Request = builder.Build();
        return this;
    }

    public void WillRespondWith(Action<IPactDefinitionInteractionResponseBuilder> action)
    {
        var builder = new PactDefinitionInteractionResponseBuilder();
        action(builder);
        _interaction.Response = builder.Build();
    }

    public PactDefinitionInteraction Build() => _interaction;
}
