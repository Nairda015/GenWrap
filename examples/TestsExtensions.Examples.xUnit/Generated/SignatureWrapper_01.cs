// using System.Text.Json;
//
// namespace TestsExtensions.Examples.xUnit.Generated;
//
// // Example of generated class for Calculator_Add test
// // ReSharper disable once UnusedType.Local
// file record SignatureWrapper : ISignatureWrapper
// {
//     public string Key => "CalculatorExample/TestData/Calculator_Add.json";
//     public int InputA { get; init; } = default!;
//     public int InputB { get; init; } = default!;
//     public int Expected { get; init; } = default!;
//
//     public IEnumerable<object[]> Deserialize(string json)
//     {
//         var datalist = JsonSerializer.Deserialize<List<SignatureWrapper>>(json);
//         if (datalist is null) return new List<object[]>();
//
//         return datalist
//             .Select(data => new object[] { data.InputA, data.InputB, data.Expected }) //order matters
//             .ToList();
//     }
// }