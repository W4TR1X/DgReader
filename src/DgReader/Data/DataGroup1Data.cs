namespace DgReader.Data;

public class DataGroup1Data : IDataGroupData
{
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
