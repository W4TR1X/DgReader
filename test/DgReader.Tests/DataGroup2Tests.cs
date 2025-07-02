namespace DgReader.Tests;

public sealed class DataGroup2Tests
{
    [Fact]
    public async Task Parse_Valid_ReturnsExpectedValues()
    {
        var content = await File.ReadAllTextAsync(
            Path.Combine("Data", "dg2-2-base64.txt"));

        // Act
        var dg2 = new DataGroup2(Convert.FromBase64String(content));
        var result = dg2.Parse();
        var values = dg2.Values;

        // Assert
        Assert.True(result);
        Assert.NotNull(values);
        Assert.NotNull(values.ImageData);
    }

    [Fact]
    public void Parse_InvalidTag_ReturnsFalse()
    {
        // Arrange: tag is wrong (0x00 instead of 0x5F1F)
        var data = new byte[] { 0x00, 0x00, 0x5A }.Concat(new byte[90]).ToArray();
        var dg2 = new DataGroup2(data);

        // Act
        var result = dg2.Parse();

        // Assert
        Assert.False(result);
        Assert.Null(dg2.Values);
    }
}
