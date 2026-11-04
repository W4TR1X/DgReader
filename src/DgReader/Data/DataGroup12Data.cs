namespace DgReader.Data;

public sealed record DataGroup12Data : IDataGroupData
{
    public string? IssuingAuthority { get; set; }
    public DateOnly? DateOfIssue { get; set; }
    public string? OtherPersonsDetails { get; set; }
    public string? EndorsmentsOrObservations { get; set; }
    public string? TaxOrExitRequirements { get; set; }
    public byte[]? FrontImage { get; set; }
    public byte[]? RearImage { get; set; }
    public DateTime? PersonalizationTime { get; set; }
    public string? PersonalizationTimeString { get; set; }
    public string? PersonalizationDeviceSerialNumber { get; set; }
}