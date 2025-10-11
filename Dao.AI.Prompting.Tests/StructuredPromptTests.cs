using FluentAssertions;

namespace Dao.AI.Prompting.Tests;

public class StructuredPromptTests
{
    [Fact]
    public async Task Constructor_ShouldHaveCorrectStructuredData_WhenProvidedDictionary()
    {
        // Arrange
        var systemPrompt = "System Prompt";
        var inputData = new Dictionary<string, TestData>
        {
            { "Data1", new TestData { Id = 1, Name = "Test1" } },
            { "Data2", new TestData { Id = 2, Name = "Test2" } }
        };
        // Act
        var structuredPrompt = new StructuredPrompt<TestData>(systemPrompt, inputData: inputData);
        // Assert
        await Verify(structuredPrompt.StructuredData);
    }

    [Fact]
    public void Constructor_ShouldInitiliazeToNullInputData_WhenEmptyDictionary()
    {
        // Act
        var structuredPrompt = new StructuredPrompt<TestData>("System Prompt");
        // Assert
        structuredPrompt.StructuredData.Should().BeEmpty();
    }
}

public class TestData
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
