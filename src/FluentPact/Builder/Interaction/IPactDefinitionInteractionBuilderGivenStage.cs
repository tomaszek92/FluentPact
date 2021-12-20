namespace FluentPact.Builder.Interaction;

public interface IPactDefinitionInteractionBuilderGivenStage
{
    IPactDefinitionInteractionBuilderUponReceivingStage Given(string state);
}
