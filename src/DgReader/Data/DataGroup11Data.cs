namespace DgReader.Data;

public sealed record DataGroup11Data : IDataGroupData
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalNumber { get; set; }
    public string DateOfBirth { get; set; }
    public string PlaceOfBirth { get; set; }
    public string Address { get; set; }
    public string Telephone { get; set; }
    public string Profession { get; set; }
    public string Title { get; set; }
    public string PersonalSummary { get; set; }
    public string ProofOfCitizenship { get; set; }
    public string TdNumbers { get; set; }
    public string CustodyInfo { get; set; }
}