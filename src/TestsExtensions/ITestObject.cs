namespace TestsExtensions;

public interface ITestObject
{
    IEnumerable<object[]> Deserialize(string json);
    public string Key { get; }
}