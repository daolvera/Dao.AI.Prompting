using System.Collections;
using System.Reflection;
using System.Text;

namespace Dao.AI.Prompting;

public static class MarkdownSerializer
{
    private const int MaxMarkdownHeaderLevel = 6;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="inputData"></param>
    /// <param name="serializerOptions"></param>
    /// <returns></returns>
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
            // todo: get the name of the property that holds this collection not the type of collection
            sb.AppendLine($"{headerLevel} {inputDataType.Name}");
            foreach (var key in dictionary.Keys)
            {
                int index = 1;
                sb.AppendLine(SerializeMarkdownRecursively(
                    key,
                    $"{headerLevel} Key {index}",
                    serializerOptions,
                    currentDepth + 1
                ));
                sb.AppendLine(SerializeMarkdownRecursively(
                    dictionary[key],
                    $"Value {index}",
                    serializerOptions,
                    currentDepth + 2
                ));
            }
            return sb.ToString();
        }
        // if string or primitive
        if (inputData is string || inputDataType.IsPrimitive || inputDataType.IsValueType)
        {
            sb.AppendLine($"{inputData}");
            return sb.ToString();
        }
        if (inputData is IEnumerable enumerable)
        {
            if (!enumerable.GetEnumerator().MoveNext() && !serializerOptions.IncludeEmptyCollections)
            {
                return string.Empty;
            }
            // todo: get the name of the property that holds this collection not the type of collection
            sb.AppendLine($"{headerLevel} {inputDataType.Name}");
            int index = 1;
            foreach (var item in enumerable)
            {
                sb.AppendLine(SerializeMarkdownRecursively(
                    item,
                    $"{index}",
                    serializerOptions,
                    currentDepth + 1
                ));
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
