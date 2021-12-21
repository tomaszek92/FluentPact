using FluentPact.Builder.Interaction;

namespace FluentPact.Builder;

public interface IPactDefinitionBuilder
{
    public interface IOptionsStage
    {
        IConsumerStage WithOptions(bool ignoreCasing, bool ignoreContractValues);
    }

    public interface IConsumerStage
    {
        IProviderStage WithConsumer(string consumer);
    }

    public interface IProviderStage
    {
        IInteractionsStage WithProvider(string provider);
    }

    public interface IInteractionsStage
    {
        IInteractionsStage WithInteraction(Action<IPactDefinitionInteractionBuilder.IGivenStage> interaction);
        IFinalStage PublishAsFile(string path);
    }

    public interface IFinalStage
    {
        Task BuildAsync(CancellationToken cancellationToken = default);
    }
}
