namespace FluentPact.Builder;

public interface IPactDefinitionBuilderFinalStage
{
    Task BuildAsync(CancellationToken cancellationToken = default);
}
