namespace FluentPact.Definitions;

public class PactDefinition
{
    public PactDefinitionConsumer Consumer { get; set; }
    public PactDefinitionProvider Provider { get; set; }
    public ICollection<PactDefinitionInteraction> Interactions { get; set; } = new List<PactDefinitionInteraction>();
    public PactDefinitionOptions Options { get; set; }
}
