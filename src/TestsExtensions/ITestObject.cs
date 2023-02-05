namespace TestsExtensions;

public interface ITestObject
{
    IEnumerable<object[]> Deserialize(string json);
}