namespace Dao.AI.Prompting.Tests;

public class MarkdownSerializerTests
{
    #region IEnumerable Tests

    [Fact]
    public async Task Serialize_EmptyList_WithIncludeEmptyCollections_ReturnsHeader()
    {
        // Arrange
        var emptyList = new List<string>();
        var options = new MarkdownSerializerOptions { IncludeEmptyCollections = true };

        // Act
        var result = MarkdownSerializer.Serialize(emptyList, "my list", options);

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_EmptyList_WithoutIncludeEmptyCollections_ReturnsEmpty()
    {
        // Arrange
        var emptyList = new List<string>();
        var options = new MarkdownSerializerOptions { IncludeEmptyCollections = false };

        // Act
        var result = MarkdownSerializer.Serialize(emptyList, "Empty collection", options);

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_ListWithItems_ReturnsFormattedMarkdown()
    {
        // Arrange
        var list = new List<string> { "item1", "item2", "item3" };
        var options = new MarkdownSerializerOptions();

        // Act
        var result = MarkdownSerializer.Serialize(list, "My List", options);

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_Array_ReturnsFormattedMarkdown()
    {
        // Arrange
        var array = new[] { 1, 2, 3 };
        var options = new MarkdownSerializerOptions();

        // Act
        var result = MarkdownSerializer.Serialize(array, "my array", options);

        // Assert
        await Verify(result);
    }

    #endregion

    #region Null Tests

    [Fact]
    public async Task Serialize_NullValue_WithIncludeNullValues_ReturnsNullMessage()
    {
        // Arrange
        string? nullString = null;
        var options = new MarkdownSerializerOptions { IncludeNullVaLues = true };

        // Act
        var result = MarkdownSerializer.Serialize(nullString, "null test", options);

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_NullValue_WithoutIncludeNullValues_ReturnsEmpty()
    {
        // Arrange
        string? nullString = null;
        var options = new MarkdownSerializerOptions { IncludeNullVaLues = false };

        // Act
        var result = MarkdownSerializer.Serialize(nullString, "null test", options);

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_NullValue_DefaultOptions_ReturnsEmpty()
    {
        // Arrange
        string? nullString = null;

        // Act
        var result = MarkdownSerializer.Serialize(nullString, "Null string test");

        // Assert
        await Verify(result);
    }

    #endregion

    #region IDictionary Tests

    [Fact]
    public async Task Serialize_EmptyDictionary_WithIncludeEmptyCollections_ReturnsHeader()
    {
        // Arrange
        var emptyDict = new Dictionary<string, string>();
        var options = new MarkdownSerializerOptions { IncludeEmptyCollections = true };

        // Act
        var result = MarkdownSerializer.Serialize(emptyDict, "empty test", options);

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_EmptyDictionary_WithoutIncludeEmptyCollections_ReturnsEmpty()
    {
        // Arrange
        var emptyDict = new Dictionary<string, string>();
        var options = new MarkdownSerializerOptions { IncludeEmptyCollections = false };

        // Act
        var result = MarkdownSerializer.Serialize(emptyDict, "empty dictionary", options);

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_DictionaryWithItems_ReturnsFormattedMarkdown()
    {
        // Arrange
        var dict = new Dictionary<string, int>
        {
            { "key1", 1 },
            { "key2", 2 }
        };
        var options = new MarkdownSerializerOptions();

        // Act
        var result = MarkdownSerializer.Serialize(dict, "full dictionary", options);

        // Assert
        await Verify(result);
    }

    #endregion

    #region Primitive Tests

    [Fact]
    public async Task Serialize_IntegerPrimitive_ReturnsFormattedString()
    {
        // Arrange
        int value = 42;

        // Act
        var result = MarkdownSerializer.Serialize(value, "Primitive test");

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_BooleanPrimitive_ReturnsFormattedString()
    {
        // Arrange
        bool value = true;

        // Act
        var result = MarkdownSerializer.Serialize(value, "Bool primitive test");

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_DoublePrimitive_ReturnsFormattedString()
    {
        // Arrange
        double value = 3.14;

        // Act
        var result = MarkdownSerializer.Serialize(value, "Double test");

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_NullableIntWithValue_ReturnsFormattedString()
    {
        // Arrange
        int? value = 123;

        // Act
        var result = MarkdownSerializer.Serialize(value, "int test");

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_NullableIntWithNull_WithIncludeNullValues_ReturnsNullMessage()
    {
        // Arrange
        int? value = null;
        var options = new MarkdownSerializerOptions { IncludeNullVaLues = true };

        // Act
        var result = MarkdownSerializer.Serialize(value, "null test", options);

        // Assert
        await Verify(result);
    }

    #endregion

    #region String Tests

    [Fact]
    public async Task Serialize_NonEmptyString_ReturnsFormattedString()
    {
        // Arrange
        string value = "Hello World";

        // Act
        var result = MarkdownSerializer.Serialize(value, "String test");

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_EmptyString_ReturnsFormattedString()
    {
        // Arrange
        string value = "";

        // Act
        var result = MarkdownSerializer.Serialize(value, "Test");

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_StringWithSpecialCharacters_ReturnsFormattedString()
    {
        // Arrange
        string value = "Hello\nWorld\t!";

        // Act
        var result = MarkdownSerializer.Serialize(value, "Test");

        // Assert
        await Verify(result);
    }

    #endregion

    #region Object Tests

    [Fact]
    public async Task Serialize_SimpleObject_ReturnsFormattedMarkdown()
    {
        // Arrange
        var obj = new { Name = "John", Age = 30 };

        // Act
        var result = MarkdownSerializer.Serialize(obj, "Test");

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_ObjectWithNullProperty_WithIncludeNullValues_ReturnsNullMessage()
    {
        // Arrange
        var obj = new TestClass { Name = "John", Description = null };
        var options = new MarkdownSerializerOptions { IncludeNullVaLues = true };

        // Act
        var result = MarkdownSerializer.Serialize(obj, "Test", options);

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_ObjectWithNullProperty_WithoutIncludeNullValues_SkipsNull()
    {
        // Arrange
        var obj = new TestClass { Name = "John", Description = null };
        var options = new MarkdownSerializerOptions { IncludeNullVaLues = false };

        // Act
        var result = MarkdownSerializer.Serialize(obj, "Test", options);

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_NestedObject_ReturnsNestedMarkdown()
    {
        // Arrange
        var obj = new
        {
            Person = new { Name = "John", Age = 30 },
            Address = new { Street = "123 Main St", City = "Anytown" }
        };

        // Act
        var result = MarkdownSerializer.Serialize(obj, "Test");

        // Assert
        await Verify(result);
    }

    #endregion

    #region Header Level Tests

    [Fact]
    public async Task Serialize_NestedCollections_UsesCorrectHeaderLevels()
    {
        // Arrange
        var list = new List<Dictionary<string, string>>
        {
            new() { { "key1", "value1" } }
        };

        // Act
        var result = MarkdownSerializer.Serialize(list, "Test");

        // Assert
        await Verify(result);
    }

    [Fact]
    public async Task Serialize_DeepNesting_CapsHeaderLevelAtSix()
    {
        // Arrange - Create a deeply nested structure
        var level1 = new List<List<List<List<List<List<string>>>>>>();
        var level2 = new List<List<List<List<List<string>>>>>();
        var level3 = new List<List<List<List<string>>>>();
        var level4 = new List<List<List<string>>>();
        var level5 = new List<List<string>>();
        var level6 = new List<string> { "deep value" };

        level5.Add(level6);
        level4.Add(level5);
        level3.Add(level4);
        level2.Add(level3);
        level1.Add(level2);

        // Act
        var result = MarkdownSerializer.Serialize(level1, "Test");

        // Assert
        await Verify(result);
    }

    #endregion

    // Helper class for testing
    private class TestClass
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
