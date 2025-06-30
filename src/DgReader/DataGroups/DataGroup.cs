namespace DgReader.DataGroups;

public abstract class DataGroup<T> where T : IDataGroupData
{
    public byte[] Data { get; private set; }
    public byte[] Body { get; private set; }
    public int Position { get; internal set; }
    public string Type { get; private set; }

    protected DataGroup(string type, byte[] data)
    {
        Data = data;
        Position = 1;
        Type = type;

        try
        {
            GetNextLength();
        }
        catch { }

        Body = Data[Position..];
    }

    public string Hash
        => Convert.ToHexString(SHA256.HashData(Data));

    public abstract bool Parse();
    public bool TryParse()
    {
        try
        {
            return Parse();
        }
        catch
        {
            return false;
        }
    }

    public abstract T? Values { get; }

    protected int GetNextLength()
    {
        var end = Data.Length > Position + 4
            ? Position + 4
            : Data.Length;

        var (length, lengthOffset) = Asn1Length(Data[Position..end]);
        Position += lengthOffset;

        return length;
    }

    protected byte[] GetNextValue()
    {
        var length = GetNextLength();
        var value = Data[Position..(Position + length)];

        Position += length;
        return value;
    }

    protected int GetNextTag()
    {
        if (Data.Length < Position)
            throw new Exception("Invalid tag");

        var tag = 0;

        var hex = BinToHex(Data[Position]);
        if ((hex & 0x0F) == 0x0F)
        {
            tag = BinToHex(Data[Position..(Position + 2)]);
            Position += 2;
        }
        else
        {
            tag = Data[Position];
            Position += 1;
        }

        return tag;
    }

    protected virtual bool VerifyTag(int tag, int expectedTag)
        => tag == expectedTag;

    private static (int Length, int LengthOffset) Asn1Length(byte[] data)
    {
        if (data[0] < 0x80)
        {
            return (BinToHex(data[0]), 1);
        }

        if (data[0] == 0x81)
        {
            return (BinToHex(data[1]), 2);
        }

        if (data[0] == 0x82)
        {
            return (BinToHex(data[1..3]), 3);
        }

        throw new Exception("Invalid length");
    }

    private static int BinToHex(byte b)
    {
        return int.Parse(b.ToString("X2"), System.Globalization.NumberStyles.HexNumber);
    }

    private static int BinToHex(byte[] val)
    {
        string hexString = BinToHexRep(val);
        // UInt64.Parse handles parsing a hexadecimal string to a ulong (UInt64)
        return int.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
    }

    private static string BinToHexRep(byte[] val)
    {
        // Check for null or empty array to avoid issues
        if (val == null || val.Length == 0)
        {
            return "0"; // Or throw an exception, depending on desired behavior for empty input
        }

        return BitConverter.ToString(val).Replace("-", "");
    }
}