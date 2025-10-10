namespace Dao.AI.Prompting;

public class StructuredPrompt<T>
{
    public string SystemPrompt { get; set; }
    public string? UserPrompt { get; set; }
    public string? ClosingPrompt { get; set; }
    public string? StructuredData { get; set; }

    public StructuredPrompt(
        string systemPrompt,
        string? userPrompt = null,
        string? closingPrompt = null,
        IDictionary<string, T>? inputData = null,
        MarkdownSerializerOptions? markdownSerializerOptions = null
    )
    {
        SystemPrompt = systemPrompt;
        UserPrompt = userPrompt;
        ClosingPrompt = closingPrompt;
        StructuredData = MarkdownSerializer.Serialize(inputData, "Input Data", markdownSerializerOptions);
    }
}
