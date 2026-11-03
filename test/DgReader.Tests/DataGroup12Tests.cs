namespace DgReader.Tests;

public class DataGroup12Tests
{
    [Fact]
    public async Task Parse_ValidData_ReturnsExpectedValuez()
    {
        var content = await File.ReadAllTextAsync(
            Path.Combine("Data", "dg12-base64.txt"));

        // Act
        var dg12 = new DataGroup12(Convert.FromBase64String(content));
        var result = dg12.Parse();
        var values = dg12.Values;

        // Assert
        Assert.True(result);
        Assert.NotNull(values);
    }

    [Fact]
    public async Task Parse_ValidData_ReturnsExpectedValues()
    {
        var content = await File.ReadAllTextAsync(
            Path.Combine("Data", "dg12-base64.txt"));

        // Act
        var dg12 = new DataGroup12(Convert.FromBase64String(content));
        var result = dg12.Parse();
        var values = dg12.Values;

        // Assert
        Assert.True(result);
        Assert.NotNull(values);
    }

    [Fact]
    public void Parse_InvalidTag_ReturnsFalse()
    {
        // Arrange: tag is wrong (0x00 instead of 0x5C)
        var data = new byte[] { 0x00, 0x00, 0x00 };
        var dg12 = new DataGroup12(data);

        // Act
        var result = dg12.Parse();

        // Assert
        Assert.False(result);
        Assert.Null(dg12.Values);
    }
}
