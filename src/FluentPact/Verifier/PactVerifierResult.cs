namespace FluentPact.Verifier;

public class PactVerifierResult
{
    public bool IsSuccessful => Interactions.All(x => x.IsSuccessful);
    public IEnumerable<PactVerifierInteractionResult> Interactions => _interactions.AsEnumerable();

    private readonly List<PactVerifierInteractionResult> _interactions = new();

    public void AddInteraction(PactVerifierInteractionResult interactionResult)
    {
        _interactions.Add(interactionResult);
    }
}
