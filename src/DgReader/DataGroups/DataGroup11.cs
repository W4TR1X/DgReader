namespace DgReader.DataGroups;

public sealed class DataGroup11 : DataGroup<DataGroup11Data>
{
    public DataGroup11(byte[] data) : base("DG11", data) { }

    private DataGroup11Data? _values;
    public override DataGroup11Data? Values => _values;

    public override bool Parse()
    {
        _values = null;

        var tag = GetNextTag();
        if (!VerifyTag(tag, 0x5C))
            return false;

        GetNextValue();

        var values = new DataGroup11Data();

        while (Position < Data.Length)
        {
            tag = GetNextTag();

            var val = GetNextValue();
            var value = Encoding.UTF8.GetString(val);

            switch (tag)
            {
                case 0x5F0E: (values.FirstName, values.LastName) = RetrieveName(value); break;
                case 0x5F10: values.PersonalNumber = value; break;
                case 0x5F2B: values.DateOfBirth = value; break;
                case 0x5F11: values.PlaceOfBirth = value; break;
                case 0x5F42: values.Address = value; break;
                case 0x5F12: values.Telephone = value; break;
                case 0x5F13: values.Profession = value; break;
                case 0x5F14: values.Title = value; break;
                case 0x5F15: values.PersonalSummary = value; break;
                case 0x5F16: values.ProofOfCitizenship = value; break;
                case 0x5F17: values.TdNumbers = value; break;
                case 0x5F18: values.CustodyInfo = value; break;
                default: break;
            }
        }

        _values = values;
        return true;
    }

    private static (string FirstName, string LastName) RetrieveName(string value)
    {
        var firstName = value[(value.LastIndexOf("<<") + 2)..].Replace("<", " ");
        var lastName = value[..(value.LastIndexOf("<<"))];

        return (firstName, lastName);
    }
}