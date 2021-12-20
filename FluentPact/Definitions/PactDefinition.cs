namespace FluentPact.Definitions;

public class PactDefinition
{
    public PactDefinitionConsumer Consumer { get; init; }
    public PactDefinitionProvider Provider { get; init; }
    public IEnumerable<PactDefinitionInteraction> Interactions { get; init; } = new List<PactDefinitionInteraction>();
    public PactDefinitionOptions Options { get; init; }
}

public class PactDefinitionOptions
{
    public bool IgnoreCasing { get; init; }
    public bool IgnoreContractValues { get; init; }
}
