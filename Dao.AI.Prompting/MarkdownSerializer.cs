using System.Collections;
using System.Reflection;
using System.Text;

namespace Dao.AI.Prompting;

public static class MarkdownSerializer
{
    private const int MaxMarkdownHeaderLevel = 6;

    /// <summary>
    /// Translates an object into a markdown representation.
    /// Representations will include the property names and values of the object.
    /// Objects at the top level will be represented as H1 headers, 
    /// with each subsequent level of nesting represented as a lower level header.
    /// </summary>
    /// <param name="inputData">
    /// The object to translate to markdown
    /// </param>
    /// <param name="serializerOptions">Provides different ways to edit the markdown behavior</param>
    /// <returns>a markdown string</returns>
    public static string Serialize<T>(
        T inputData,
        string objectName,
        MarkdownSerializerOptions? serializerOptions = null
    )
    {
        serializerOptions ??= new();
        return SerializeMarkdownRecursively(inputData, objectName, serializerOptions);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="inputData"></param>
    /// <param name="serializerOptions"></param>
    /// <param name="currentDepth"></param>
    /// <returns></returns>
    private static string SerializeMarkdownRecursively<T>(
        T inputData,
        string propertyName,
        MarkdownSerializerOptions serializerOptions,
        int currentDepth = 0
    )
    {
        if (inputData is null)
        {
            return serializerOptions.IncludeNullVaLues ? $"{propertyName}: Null" : string.Empty;
        }
        var sb = new StringBuilder();
        string headerLevel = new('#', Math.Min(currentDepth + 1, MaxMarkdownHeaderLevel));

        var inputDataType = inputData.GetType();
        // if dictionary
        if (inputData is IDictionary dictionary)
        {
            if (dictionary.Count == 0 && !serializerOptions.IncludeEmptyCollections)
            {
                return string.Empty;
            }
            sb.AppendLine($"{headerLevel} {propertyName}");
            foreach (var key in dictionary.Keys)
            {
                int index = 1;
                sb.AppendLine(SerializeMarkdownRecursively(
                    key,
                    $"{index}. Key ",
                    serializerOptions,
                    currentDepth + 1
                ));
                sb.AppendLine(SerializeMarkdownRecursively(
                    dictionary[key],
                    $"{index}. Value ",
                    serializerOptions,
                    currentDepth + 2
                ));
            }
            return sb.ToString();
        }
        // if string or primitive
        if (inputData is string || inputDataType.IsPrimitive || inputDataType.IsValueType)
        {
            sb.Append($"{inputData}");
            return sb.ToString();
        }
        if (inputData is IEnumerable enumerable)
        {
            if (!enumerable.GetEnumerator().MoveNext() && !serializerOptions.IncludeEmptyCollections)
            {
                return string.Empty;
            }
            sb.AppendLine($"{headerLevel} {propertyName}");
            int index = 1;
            foreach (var item in enumerable)
            {
                sb.AppendLine($"- {SerializeMarkdownRecursively(
                    item,
                    $"{index}.",
                    serializerOptions,
                    currentDepth + 1
                )}");
                index++;
            }
            return sb.ToString();
        }
        // if object
        var properties = inputDataType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in properties)
        {
            sb.AppendLine(SerializeMarkdownRecursively(
                property.GetValue(inputData),
                property.Name,
                serializerOptions,
                currentDepth + 1
            ));
        }
        return sb.ToString();
    }
}
