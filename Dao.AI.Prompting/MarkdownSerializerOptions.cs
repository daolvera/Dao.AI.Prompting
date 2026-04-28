namespace Dao.AI.Prompting;

public class MarkdownSerializerOptions
{
    public bool IncludeNullValues { get; set; }
    public bool IncludeEmptyCollections { get; set; } = true;
    public int MaxDepth
    {
        get => field;
        set => field = value is >= 1 and <= 15
            ? value
            : throw new ArgumentOutOfRangeException(nameof(MaxDepth), "MaxDepth must be between 1 and 15.");
    } = 6;
}
