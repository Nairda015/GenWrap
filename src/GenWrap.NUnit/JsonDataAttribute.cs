// using System.Collections;
// using GenWrap.Abstraction.Internal;
// using GenWrap.Abstraction.Internal.Exceptions;
// using GenWrap.Abstraction.Internal.Extensions;
// using NUnit.Framework.Interfaces;
//
// namespace GenWrap.NUnit;
//
// /// <summary>
// /// This is entry point for source generator
// /// </summary>
// public class JsonDataAttribute : IParameterDataSource
// {
//     private readonly string _filePath;
//     public JsonDataAttribute(string filePath) => _filePath = filePath;
//
//     /// <inheritDoc />
//     public IEnumerable GetData(IParameterInfo parameter)
//     {
//         var fileData = _filePath.GetJsonFileData();
//         if (string.IsNullOrWhiteSpace(fileData)) throw new FileIsEmptyException(_filePath);
//
//         return SignatureWrapperStore.TryGetValue(_filePath, out var wrapper)
//             ? wrapper!.Deserialize(fileData)
//             : throw new AssemblyScanningException();
//     }
// }