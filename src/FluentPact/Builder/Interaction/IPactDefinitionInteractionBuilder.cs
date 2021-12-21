using FluentPact.Builder.Interaction.Request;
using FluentPact.Builder.Interaction.Response;

namespace FluentPact.Builder.Interaction;

public interface IPactDefinitionInteractionBuilder
{
    public interface IGivenStage
    {
        IUponReceivingStage Given(string state);
    }

    public interface IUponReceivingStage
    {
        IWithStage UponReceiving(string description);
    }

    public interface IWithStage
    {
        IWillRespondWithStage With(Action<IPactDefinitionInteractionRequestBuilder> buildRequest);
    }

    public interface IWillRespondWithStage
    {
        void WillRespondWith(Action<IPactDefinitionInteractionResponseBuilder> buildResponse);
    }
}
