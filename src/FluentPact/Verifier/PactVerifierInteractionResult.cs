namespace FluentPact.Verifier;

public class PactVerifierInteractionResult
{
    public bool IsSuccessful => !Errors.Any();
    public IEnumerable<string> Errors => _errors.AsEnumerable();

    private readonly List<string> _errors = new();

    public void AddError(string errorMessage)
    {
        _errors.Add(errorMessage);
    }
}
