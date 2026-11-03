namespace DgReader.DataGroups;

public sealed class DataGroup12(byte[] data) : DataGroup<DataGroup12Data>("DG12", data)
{
    private DataGroup12Data? _values;
    public override DataGroup12Data? Values => _values;

    public override bool Parse()
    {
        _values = null;

        var tag = GetNextTag();
        if (!VerifyTag(tag, 0x5C))
            return false;

        GetNextValue();

        var values = new DataGroup12Data();

        while (Position < Data.Length)
        {
            tag = GetNextTag();

            var val = GetNextValue();
            var value = Encoding.UTF8.GetString(val);

            switch (tag)
            {
                case 0x5F19:
                    values.IssuingAuthority = value;
                    break;

                case 0x5F26:
                    try
                    {
                        if (value.Length == 8)
                        {
                            values.DateOfIssue =
                                DateOnly.ParseExact(value, "yyyyMMdd", CultureInfo.InvariantCulture);
                        }
                        else if (value.Length == 6)
                        {
                            values.DateOfIssue =
                                DateOnly.ParseExact(value, "yyMMdd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            values.DateOfIssue = null;
                        }
                    }
                    catch (Exception)
                    {
                        values.DateOfIssue = null;
                    }

                    break;

                case 0xA0:
                    // not implemented yet
                    break;

                case 0x5F1B:
                    values.EndorsmentsOrObservations = value;
                    break;

                case 0x5F1C:
                    values.TaxOrExitRequirements = value;
                    break;

                case 0x5F1D:
                    values.FrontImage = val;
                    break;

                case 0x5F1E:
                    values.RearImage = val;
                    break;

                case 0x5F55:

                    values.PersonalizationTimeString = value;

                    try
                    {
                        values.PersonalizationTime = DateTime.ParseExact(value, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        values.PersonalizationTime = null;
                    }

                    break;

                case 0x5F56:
                    values.PersonalizationDeviceSerialNumber = value;
                    break;

                default: break;
            }
        }

        _values = values;
        return true;
    }
}