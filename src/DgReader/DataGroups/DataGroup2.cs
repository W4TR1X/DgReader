
using Org.BouncyCastle.Tls;
using System.IO;

namespace DgReader.DataGroups;

public sealed class DataGroup2 : DataGroup<DataGroup2Data>
{
    private DataGroup2Data? _values;
    public override DataGroup2Data? Values => _values;

    private readonly static byte[] JpegHeader = [0xff, 0xd8, 0xff, 0xe0, 0x00, 0x10, 0x4a, 0x46, 0x49, 0x46];
    private readonly static byte[] Jpeg2000BitmapHeader = [0x00, 0x00, 0x00, 0x0c, 0x6a, 0x50, 0x20, 0x20, 0x0d, 0x0a];
    private readonly static byte[] Jpeg2000CodestreamBitmapHeader = [0xff, 0x4f, 0xff, 0x51];

    public DataGroup2(byte[] data) : base("DG2", data) { }

    public override bool Parse()
    {
        _values = null;

        var tag = GetNextTag();
        if (!VerifyTag(tag, 0x7F61))
            return false;

        GetNextLength();

        var values = new DataGroup2Data();

        tag = GetNextTag();
        if (VerifyTag(tag, 0x02))
        {
            values.NumberOfImages = GetNextValue()[0];
        }

        tag = GetNextTag();
        if (!VerifyTag(tag, 0x7F60))
            return false;

        GetNextLength();

        tag = GetNextTag();
        if (!VerifyTag(tag, 0xA1))
            return false;

        GetNextValue();

        tag = GetNextTag();
        if (!VerifyTag(tag, [0x5F2E, 0x7F2E]))
            return false;

        if (ParseIso19794_5(values, GetNextValue())){
            _values = values;
            return true;
        }

        return false;
    }

    private bool ParseIso19794_5(DataGroup2Data values, byte[] data)
    {
        if (data[0] != 0x46 && data[1] != 0x41
         && data[2] != 0x43 && data[3] != 0x00)
            return false;

        int offset = 4;

        values.VersionNumber = BinToInt(data[offset..(offset + 4)]);
        offset += 4;

        values.LengthOfRecord = BinToInt(data[offset..(offset + 4)]);
        offset += 4;

        values.NumberOfFacialImages = BinToInt(data[offset..(offset + 2)]);
        offset += 2;

        values.FacialRecordDataLength = BinToInt(data[offset..(offset + 4)]);
        offset += 4;

        values.NumberOfFeaturePoints = BinToInt(data[offset..(offset + 2)]);
        offset += 2;

        values.Gender = BinToInt(data[offset..(offset + 1)]);
        offset += 1;

        values.EyeColor = BinToInt(data[offset..(offset + 1)]);
        offset += 1;

        values.HairColor = BinToInt(data[offset..(offset + 1)]);
        offset += 1;

        values.FeatureMask = BinToInt(data[offset..(offset + 3)]);
        offset += 3;

        values.Expression = BinToInt(data[offset..(offset + 2)]);
        offset += 2;

        values.PoseAngle = BinToInt(data[offset..(offset + 3)]);
        offset += 3;

        values.PoseAngleUncertainty = BinToInt(data[offset..(offset + 3)]);
        offset += 3;

        offset += values.NumberOfFeaturePoints * 8;

        values.FaceImageType = BinToInt(data[offset..(offset + 1)]);
        offset += 1;

        values.ImageDataType = BinToInt(data[offset..(offset + 1)]);
        offset += 1;

        values.ImageWidth = BinToInt(data[offset..(offset + 2)]);
        offset += 2;

        values.ImageHeight = BinToInt(data[offset..(offset + 2)]);
        offset += 2;

        values.ImageColorSpace = BinToInt(data[offset..(offset + 1)]);
        offset += 1;

        values.SourceType = BinToInt(data[offset..(offset + 1)]);
        offset += 1;

        values.DeviceType = BinToInt(data[offset..(offset + 2)]);
        offset += 2;

        values.Quality = BinToInt(data[offset..(offset + 2)]);
        offset += 2;

        if (data.Length < offset + Jpeg2000CodestreamBitmapHeader.Length)
            return false;

        if (!data[offset..(offset + JpegHeader.Length)].SequenceEqual(JpegHeader)
         && !data[offset..(offset + Jpeg2000BitmapHeader.Length)].SequenceEqual(Jpeg2000BitmapHeader)
         && !data[offset..(offset + Jpeg2000CodestreamBitmapHeader.Length)].SequenceEqual(Jpeg2000CodestreamBitmapHeader))
        {
            return false;
        }

        values.ImageData = data[offset..];

        if(values.ImageData is { Length: > 0 } )
            return true;

        return false;
    }
}
