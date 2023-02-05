namespace TestsExtensions;

public interface ISignatureWrapper
{
    IEnumerable<object[]> Deserialize(string json);
    public string Key { get; }
}