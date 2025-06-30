# DgReader

DgReader is a C# library for parsing and processing ICAO-compliant Data Groups found in electronic Machine Readable Travel Documents (eMRTDs) such as ePassports and ID cards.

**Currently, this library only supports parsing DG1 (MRZ) with the TD1 variant and DG11.**  
It does not support other data group types or other MRZ formats (such as TD2 or TD3).

## Features

- **ASN.1 Parsing**: Handles TLV/ASN.1 encoded data groups.
- **Data Group Abstractions**: Provides base and specialized classes for DG1 (TD1) and DG11.
- **Data Extraction**: Extracts structured data such as name, document number, nationality, date of birth, address, and more.
- **Error Handling**: Safe parsing methods and tag verification.
- **Hash Calculation**: SHA256 hashing for data integrity.

## Example Usage

```csharp
// Example: Parsing DG1 data group (TD1 variant)
var dg1Bytes = ... // Load DG1 bytes from eMRTD
var dg1 = new DataGroup1(dg1Bytes);
if (dg1.TryParse())
{
    var data = dg1.Values;
    Console.WriteLine($"Document Number: {data.DocumentNumber}");
    Console.WriteLine($"Name: {data.FirstName} {data.LastName}");
}

// Example: Parsing DG11 data group
var dg11Bytes = ... // Load DG11 bytes from eMRTD
var dg11 = new DataGroup11(dg11Bytes);
if (dg11.TryParse())
{
    var data = dg11.Values;
    Console.WriteLine($"Full Name: {data.FirstName} {data.LastName}");
    Console.WriteLine($"Address: {data.Address}");
}
```

## Supported Data Groups

- **DG1 (TD1 only)**: MRZ (Machine Readable Zone) info: document type, number, names, nationality, date of birth, etc.
- **DG11**: Additional personal details: full name, address, place of birth, etc.

> **Note**: Other data group types and MRZ formats (such as TD2 or TD3) are not supported.

## Installation

Clone the repository and add the project to your solution:

```bash
git clone https://github.com/W4TR1X/DgReader.git
```

## License

This project is licensed under the [MIT License](LICENSE.txt).

## Contributing

Contributions are welcome! Please open issues or pull requests with improvements.

## Authors

[W4TR1X](https://github.com/W4TR1X)  
[batuhanoztrk](https://github.com/batuhanoztrk)
