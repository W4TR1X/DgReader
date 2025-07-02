namespace DgReader.Data;

public sealed record DataGroup2Data : IDataGroupData
{
    public int NumberOfImages { get; set; }
    public int VersionNumber { get; set; }
    public int LengthOfRecord { get; set; }
    public int NumberOfFacialImages { get; set; }
    public int FacialRecordDataLength { get; set; }
    public int NumberOfFeaturePoints { get; set; }
    public int Gender { get; set; }
    public int EyeColor { get; set; }
    public int HairColor { get; set; }
    public int FeatureMask { get; set; }
    public int Expression { get; set; }
    public int PoseAngle { get; set; }
    public int PoseAngleUncertainty { get; set; }
    public int FaceImageType { get; set; }
    public int ImageDataType { get; set; }
    public int ImageWidth { get; set; }
    public int ImageHeight { get; set; }
    public int ImageColorSpace { get; set; }
    public int SourceType { get; set; }
    public int DeviceType { get; set; }
    public int Quality { get; set; }
    public byte[] ImageData { get; set; }
}