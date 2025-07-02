namespace DgReader.Tests;

public sealed class DataGroup1Tests
{
    [Fact]
    public async Task Parse_ValidTD1Data_ReturnsExpectedValues()
    {
        var content = await File.ReadAllTextAsync(
            Path.Combine("Data", "dg1-td1-base64.txt"));

        // Act
        var dg1 = new DataGroup1(Convert.FromBase64String(content));
        var result = dg1.Parse();
        var values = dg1.Values;

        // Assert
        Assert.True(result);
        Assert.NotNull(values);
        Assert.NotNull(values.Mrz);
    }

    [Fact]
    public async Task Parse_ValidTD2Data_ReturnsExpectedValues()
    {
        var content = await File.ReadAllTextAsync(
            Path.Combine("Data", "dg1-td2-base64.txt"));

        // Act
        var dg1 = new DataGroup1(Convert.FromBase64String(content));
        var result = dg1.Parse();
        var values = dg1.Values;

        // Assert
        Assert.True(result);
        Assert.NotNull(values);
        Assert.NotNull(values.Mrz);
    }

    [Fact]
    public async Task Parse_ValidOtherData_ReturnsExpectedValues()
    {
        var content = await File.ReadAllTextAsync(
            Path.Combine("Data", "dg1-other-base64.txt"));

        // Act
        var dg1 = new DataGroup1(Convert.FromBase64String(content));
        var result = dg1.Parse();
        var values = dg1.Values;

        // Assert
        Assert.True(result);
        Assert.NotNull(values);
        Assert.NotNull(values.Mrz);
    }

    [Fact]
    public void Parse_InvalidTag_ReturnsFalse()
    {
        // Arrange: tag is wrong (0x00 instead of 0x5F1F)
        var data = new byte[] { 0x00, 0x00, 0x5A }.Concat(new byte[90]).ToArray();
        var dg1 = new DataGroup1(data);

        // Act
        var result = dg1.Parse();

        // Assert
        Assert.False(result);
        Assert.Null(dg1.Values);
    }
}
