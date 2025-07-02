namespace DgReader.Data;

public sealed record DataGroup1Data : IDataGroupData
{
    public MrzDocumentType MrzDocumentType { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string DocumentNumber { get; set; }
    public string DocumentSubType { get; set; }
    public string DocumentType { get; set; }
    public string FirstName { get; set; }
    public string Gender { get; set; }
    public string IdentityNumber { get; set; }
    public string IssuingAuthority { get; set; }
    public string LastName { get; set; }
    public string Mrz { get; set; }
    public string Nationality { get; set; }
    public DateOnly ValidUntil { get; set; }
}

public enum MrzDocumentType
{
    TD1,
    TD2,
    Other
}