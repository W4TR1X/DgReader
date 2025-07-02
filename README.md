# DgReader
 
DgReader is a C# library for parsing and processing ICAO-compliant Data Groups found in electronic Machine Readable Travel Documents (eMRTDs) such as ePassports and ID cards.
 
## NuGet
 
[![NuGet](https://img.shields.io/nuget/v/DgReader.svg)](https://www.nuget.org/packages/DgReader)
 
Install via NuGet Package Manager:
dotnet add package DgReader
[View on NuGet.org](https://www.nuget.org/packages/DgReader)
 
**This library supports parsing DG1, DG2, and DG11.**  
 
## Features
 
- **ASN.1 Parsing**: Handles TLV/ASN.1 encoded data groups.
- **Data Group Abstractions**: Provides base and specialized classes for DG1 (TD1, TD2, Other), DG2 (Biometric Face Image), and DG11.
- **Data Extraction**: Extracts structured data such as name, document number, nationality, date of birth, address, biometric face image, and more.
- **Error Handling**: Safe parsing methods and tag verification.
- **Hash Calculation**: SHA256 hashing for data integrity.
 
## Example Usage
 
```csharp
// Example: Parsing DG1 data group (TD1, TD2, or Other variant)
var dg1Bytes = ... // Load DG1 bytes from eMRTD
var dg1 = new DataGroup1(dg1Bytes);
if (dg1.Parse())
{
    var data = dg1.Values;
    Console.WriteLine($"Document Number: {data.DocumentNumber}");
    Console.WriteLine($"Name: {data.FirstName} {data.LastName}");
    Console.WriteLine($"MRZ Type: {data.MrzDocumentType}"); // TD1, TD2, or Other
}
 
// Example: Parsing DG11 data group
var dg11Bytes = ... // Load DG11 bytes from eMRTD
var dg11 = new DataGroup11(dg11Bytes);
if (dg11.Parse())
{
    var data = dg11.Values;
    Console.WriteLine($"Full Name: {data.FirstName} {data.LastName}");
    Console.WriteLine($"Address: {data.Address}");
}
 
// Example: Parsing DG2 data group (Biometric Face Image)
var dg2Bytes = ... // Load DG2 bytes from eMRTD
var dg2 = new DataGroup2(dg2Bytes);
if (dg2.Parse())
{
    var data = dg2.Values;
    Console.WriteLine($"Image Width: {data.ImageWidth}");
    Console.WriteLine($"Image Height: {data.ImageHeight}");
    Console.WriteLine($"Image Data Length: {data.ImageData?.Length}");
    // You can save or process the image data (JPEG/JPEG2000)
}
```
 
## Supported Data Groups
 
- **DG1 (TD1, TD2, Other)**: MRZ (Machine Readable Zone) info: document type, number, names, nationality, date of birth, etc. Supports TD1, TD2, and Other MRZ formats.
- **DG2 (Biometric Face Image)**: Biometric facial image (JPEG/JPEG2000) and related metadata (image size, color space, quality, etc.).
- **DG11**: Additional personal details: full name, address, place of birth, etc.
 
## License
 
This project is licensed under the [MIT License](LICENSE.txt).
 
## Contributing
 
Contributions are welcome! Please open issues or pull requests with improvements.
 
## Authors
 
[W4TR1X](https://github.com/W4TR1X)  
[batuhanoztrk](https://github.com/batuhanoztrk)
 
 
