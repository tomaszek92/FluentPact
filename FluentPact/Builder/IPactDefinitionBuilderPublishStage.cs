namespace FluentPact.Builder;

public interface IPactDefinitionBuilderPublishStage
{
    IPactDefinitionBuilderFinalStage PublishAsFile(string path);
}
