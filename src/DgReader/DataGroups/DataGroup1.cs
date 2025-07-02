namespace DgReader.DataGroups;

public sealed class DataGroup1 : DataGroup<DataGroup1Data>
{
    public DataGroup1(byte[] data) : base("DG1", data) { }

    private DataGroup1Data? _values;
    public override DataGroup1Data? Values => _values;

    public override bool Parse()
    {
        _values = null;

        var tag = GetNextTag();

        if (!VerifyTag(tag, 0x5F1F))
            return false;

        var body = GetNextValue();
        var type = getMRZType(body.Length);

        var values = new DataGroup1Data();

        //TD1
        if (type == MrzDocumentType.TD1)
        {
            var nameParts = RetrieveNameParts(Encoding.UTF8.GetString(body[60..]));
            values = values with
            {
                DocumentType = Encoding.UTF8.GetString(body[0..1]),
                DocumentSubType = Encoding.UTF8.GetString(body[1..2]),

                IssuingAuthority = Encoding.UTF8.GetString(body[2..5]),
                Nationality = Encoding.UTF8.GetString(body[45..48]),

                DocumentNumber = Encoding.UTF8.GetString(body[5..14]).Replace("<", ""),

                IdentityNumber = (Encoding.UTF8.GetString(body[15..30])
                    + Encoding.UTF8.GetString(body[48..59])).Replace("<", ""),

                Gender = Encoding.UTF8.GetString(body[37..38]),

                ValidUntil = DateOnly.ParseExact(Encoding.UTF8.GetString(body[38..44]), "yyMMdd", CultureInfo.InvariantCulture),
                DateOfBirth = DateOnly.ParseExact(Encoding.UTF8.GetString(body[30..36]), "yyMMdd", CultureInfo.InvariantCulture),

                LastName = nameParts[0].Replace("<", " "),
                FirstName = string.Join(" ", nameParts.Skip(1)
                    .Select(x => x.Replace("<", " "))
                    .Where(x => !string.IsNullOrWhiteSpace(x))),

                Mrz = Encoding.UTF8.GetString(body),
            };

            _values = values;
        }
        else if (type == MrzDocumentType.TD2)
        {
            var nameParts = RetrieveNameParts(Encoding.UTF8.GetString(body[5..36]));
            values = values with
            {
                DocumentType = Encoding.UTF8.GetString(body[0..1]),
                DocumentSubType = Encoding.UTF8.GetString(body[1..2]),

                IssuingAuthority = Encoding.UTF8.GetString(body[2..5]),
                Nationality = Encoding.UTF8.GetString(body[46..49]),

                DocumentNumber = Encoding.UTF8.GetString(body[36..45]).Replace("<", ""),

                IdentityNumber = Encoding.UTF8.GetString(body[64..71]).Replace("<", ""),

                Gender = Encoding.UTF8.GetString(body[56..57]),

                ValidUntil = DateOnly.ParseExact(
                    Encoding.UTF8.GetString(body[57..63]), "yyMMdd", CultureInfo.InvariantCulture),

                DateOfBirth = DateOnly.ParseExact(
                    Encoding.UTF8.GetString(body[49..55]), "yyMMdd", CultureInfo.InvariantCulture),

                LastName = nameParts[0].Replace("<", " "),
                FirstName = string.Join(" ", nameParts.Skip(1)
                    .Select(x => x.Replace("<", " "))
                    .Where(x => !string.IsNullOrWhiteSpace(x))),

                Mrz = Encoding.UTF8.GetString(body),
            };

            _values = values;
        }
        else
        {
            var nameParts = RetrieveNameParts(Encoding.UTF8.GetString(body[5..44]));
            values = values with
            {
                MrzDocumentType = type,

                DocumentType = Encoding.UTF8.GetString(body[0..1]),
                DocumentSubType = Encoding.UTF8.GetString(body[1..2]),

                IssuingAuthority = Encoding.UTF8.GetString(body[2..5]),
                Nationality = Encoding.UTF8.GetString(body[54..57]),

                DocumentNumber = Encoding.UTF8.GetString(body[44..53]).Replace("<", ""),

                IdentityNumber = Encoding.UTF8.GetString(body[72..86]).Replace("<", ""),

                Gender = Encoding.UTF8.GetString(body[64..65]),

                ValidUntil = DateOnly.ParseExact(
                    Encoding.UTF8.GetString(body[65..71]), "yyMMdd", CultureInfo.InvariantCulture),

                DateOfBirth = DateOnly.ParseExact(
                    Encoding.UTF8.GetString(body[57..63]), "yyMMdd", CultureInfo.InvariantCulture),

                LastName = nameParts[0].Replace("<", " "),
                FirstName = string.Join(" ", nameParts.Skip(1)
                    .Select(x => x.Replace("<", " "))
                    .Where(x => !string.IsNullOrWhiteSpace(x))),

                Mrz = Encoding.UTF8.GetString(body),
            };

            _values = values;
        }

        return true;
    }

    private static string[] RetrieveNameParts(string value)
        => value.Split("<")
            .Where(x => !string.IsNullOrEmpty(x))
            .ToArray();

    static MrzDocumentType getMRZType(int length)
        => length switch
        {
            0x5A => MrzDocumentType.TD1,
            0x48 => MrzDocumentType.TD2,
            _ => MrzDocumentType.Other,
        };


}