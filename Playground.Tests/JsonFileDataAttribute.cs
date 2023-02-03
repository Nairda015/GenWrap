using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit.Sdk;

namespace Playground.Tests;

public class JsonFileDataAttribute<T1, T2> : DataAttribute
{
    private readonly string _filePath;
    private readonly string? _propertyName;

    public JsonFileDataAttribute(string filePath)
    {
        _filePath = filePath;
    }

    public JsonFileDataAttribute(string filePath, string propertyName)
    {
        _filePath = filePath;
        _propertyName = propertyName;
    }

    /// <inheritDoc />
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));

        var path = GetAbsolutePath(_filePath);
        if (!File.Exists(path)) throw new ArgumentException($"Could not find file at path: {path}");
        var fileData = File.ReadAllText(_filePath);

        if (string.IsNullOrEmpty(_propertyName)) return GetData(fileData);

        // Only use the specified property as the data
        var allData = JObject.Parse(fileData);
        var data = allData[_propertyName]?.ToString();
        return GetData(data);
    }

    private static IEnumerable<object[]> GetData(string? jsonData)
    {
        if (jsonData is null) return new List<object[]>();
        var datalist = JsonConvert.DeserializeObject<List<TestObject<T1, T2>?>>(jsonData);
        if (datalist is null) return new List<object[]>();

        return datalist
            .Where(x => x is not null)
            .Select(data => new object[] { data!.Data, data.Result })
            .ToList();
    }

    private string GetAbsolutePath(string filePath) => Path.IsPathRooted(filePath)
        ? _filePath
        : Path.GetRelativePath(Directory.GetCurrentDirectory(), filePath);

    private class TestObject<TInput, TResult>
    {
        public TInput Data { get; set; } = default!;
        public TResult Result { get; set; } = default!;
    }
}

