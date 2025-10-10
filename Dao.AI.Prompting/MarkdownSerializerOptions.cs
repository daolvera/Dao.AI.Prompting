using System.ComponentModel.DataAnnotations;

namespace Dao.AI.Prompting;

public class MarkdownSerializerOptions
{
    public bool IncludeNullVaLues { get; set; }
    public bool IncludeEmptyCollections { get; set; } = true;
    [Range(1, 15)]
    public int MaxDepth { get; set; } = 6;
}
