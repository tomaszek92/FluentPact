namespace FluentPact.Verifier;

public interface IPactVerifierBuilder
{
    public interface IConsumerStage
    {
        IProviderStage WithConsumer(string consumer);
    }

    public interface IProviderStage
    {
        IRetrieveStage WithProvider(string provider);
    }

    public interface IRetrieveStage
    {
        IFinalStage RetrieveFromFile(string path);
    }

    public interface IFinalStage
    {
        Task<PactVerifierResult> VerifyAsync(CancellationToken cancellationToken = default);
    }
}
