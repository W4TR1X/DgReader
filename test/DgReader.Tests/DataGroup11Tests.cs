namespace DgReader.Tests;

public class DataGroup11Tests
{
    [Fact]
    public async Task Parse_ValidData_ReturnsExpectedValues()
    {
        var content = await File.ReadAllTextAsync(
            Path.Combine("Data","dg11-base64.txt"));

        // Act
        var dg11 = new DataGroup11(Convert.FromBase64String(content));
        var result = dg11.Parse();
        var values = dg11.Values;

        // Assert
        Assert.True(result);
        Assert.NotNull(values);
    }

    [Fact]
    public void Parse_InvalidTag_ReturnsFalse()
    {
        // Arrange: tag is wrong (0x00 instead of 0x5C)
        var data = new byte[] { 0x00, 0x00, 0x00 };
        var dg11 = new DataGroup11(data);

        // Act
        var result = dg11.Parse();

        // Assert
        Assert.False(result);
        Assert.Null(dg11.Values);
    }
}
