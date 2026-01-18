# HL7 Composite Data Types

This directory contains HL7 2.5 composite data type implementations. Composite types are complex data structures that contain multiple components, which may themselves contain subcomponents.

## Overview

Composite types are implemented as high-performance `readonly struct` types that support:
- **Zero-allocation parsing** using `Span<char>`
- **Strong typing** with full IntelliSense support
- **Validation** with detailed error messages
- **Formatted output** via helper methods

## Available Types

### Person Identification
- **XPN** - Extended Person Name (e.g., `DOE^JOHN^MICHAEL^JR^DR^MD`)
- **FN** - Family Name (subcomponent of XPN)
- **XCN** - Extended Composite ID and Name (for providers) [Coming Soon]

### Address and Contact
- **XAD** - Extended Address (e.g., `123 MAIN ST^^CITY^ST^12345^USA`)
- **SAD** - Street Address (subcomponent of XAD)
- **XTN** - Extended Telecommunication Number (e.g., `(555)123-4567^PRN^PH`)

### Identifiers
- **CX** - Extended Composite ID with Check Digit
- **EI** - Entity Identifier  
- **HD** - Hierarchic Designator

### Coded Values
- **CE** - Coded Element (e.g., `E11.9^Type 2 diabetes^ICD10`)
- **CWE** - Coded with Exceptions (enhanced CE)

### Dates and Ranges
- **DR** - Date Range (e.g., `20230101^20231231`)

### Quantities
- **CQ** - Composite Quantity with Units (e.g., `500^MG`)

## Usage Examples

### Parsing a Person Name (XPN)

```csharp
using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.Definitions;

// Parse from HL7 string
var nameString = "DOE^JOHN^MICHAEL^JR^DR^MD";
var xpn = new XPN();
xpn.Parse(nameString.AsSpan(), HL7Delimiters.Default);

// Access components
Console.WriteLine($"Family: {xpn.FamilyName.Surname.Value}");  // DOE
Console.WriteLine($"Given: {xpn.GivenName.Value}");            // JOHN
Console.WriteLine($"Middle: {xpn.SecondNames.Value}");         // MICHAEL
Console.WriteLine($"Suffix: {xpn.Suffix.Value}");              // JR
Console.WriteLine($"Prefix: {xpn.Prefix.Value}");              // DR
Console.WriteLine($"Degree: {xpn.Degree.Value}");              // MD

// Get formatted output
Console.WriteLine(xpn.GetFormattedName(NameFormat.FamilyGiven));  // "DOE, JOHN"
Console.WriteLine(xpn.GetFormattedName(NameFormat.GivenFamily));  // "JOHN DOE"
Console.WriteLine(xpn.GetFormattedName(NameFormat.Full));         // "DR JOHN MICHAEL DOE JR MD"

// Serialize back to HL7
var hl7String = xpn.ToHL7String(HL7Delimiters.Default);
// Result: "DOE^JOHN^MICHAEL^JR^DR^MD"
```

### Creating a Person Name

```csharp
// Using implicit conversion from string
ST givenName = "JOHN";
ST middleName = "MICHAEL";
FN familyName = "DOE";
ST suffix = "JR";
IS degree = "MD";

var xpn = new XPN(
    familyName: familyName,
    givenName: givenName,
    secondNames: middleName,
    suffix: suffix,
    degree: degree
);

// Or parse from string
XPN xpn2 = "DOE^JOHN^MICHAEL^JR";
```

### Parsing an Address (XAD)

```csharp
var addressString = "123 MAIN ST^^SPRINGFIELD^IL^62701^USA";
var xad = new XAD();
xad.Parse(addressString.AsSpan(), HL7Delimiters.Default);

// Access components
Console.WriteLine($"Street: {xad.StreetAddress.StreetOrMailingAddress.Value}");
Console.WriteLine($"City: {xad.City.Value}");
Console.WriteLine($"State: {xad.StateOrProvince.Value}");
Console.WriteLine($"Zip: {xad.ZipOrPostalCode.Value}");
Console.WriteLine($"Country: {xad.Country.Value}");

// Get formatted address
Console.WriteLine(xad.GetFormattedAddress());
// Result: "123 MAIN ST, SPRINGFIELD, IL 62701, USA"
```

### Working with Coded Elements (CE)

```csharp
var codeString = "E11.9^Type 2 diabetes mellitus^ICD10";
var ce = new CE();
ce.Parse(codeString.AsSpan(), HL7Delimiters.Default);

Console.WriteLine($"Code: {ce.Identifier.Value}");         // E11.9
Console.WriteLine($"Text: {ce.Text.Value}");               // Type 2 diabetes mellitus
Console.WriteLine($"System: {ce.NameOfCodingSystem.Value}"); // ICD10

// Display text or code
Console.WriteLine(ce.ToString());  // "Type 2 diabetes mellitus"
```

### Telephone Numbers (XTN)

```csharp
var phoneString = "(555)123-4567^PRN^PH^^^555^1234567";
var xtn = new XTN();
xtn.Parse(phoneString.AsSpan(), HL7Delimiters.Default);

Console.WriteLine($"Number: {xtn.TelephoneNumber.Value}");
Console.WriteLine($"Use: {xtn.TelecommunicationUseCode.Value}");  // PRN (Primary)
Console.WriteLine($"Type: {xtn.TelecommunicationEquipmentType.Value}"); // PH (Phone)

// Get formatted number
Console.WriteLine(xtn.GetFormattedNumber());
```

### Composite Quantity (CQ)

```csharp
var quantityString = "500^MG";
var cq = new CQ();
cq.Parse(quantityString.AsSpan(), HL7Delimiters.Default);

Console.WriteLine($"Amount: {cq.Quantity.Value}");      // 500
Console.WriteLine($"Units: {cq.Units.Text.Value}");     // MG

// Display
Console.WriteLine(cq.ToString());  // "500 MG"
```

### Extended Composite ID (CX)

```csharp
var idString = "12345^^^HOSPITAL^MR";
var cx = new CX();
cx.Parse(idString.AsSpan(), HL7Delimiters.Default);

Console.WriteLine($"ID: {cx.Id.Value}");                              // 12345
Console.WriteLine($"Authority: {cx.AssigningAuthority.NamespaceId.Value}"); // HOSPITAL  
Console.WriteLine($"Type: {cx.IdentifierTypeCode.Value}");           // MR
```

### Validation

All composite types support validation:

```csharp
var xpn = new XPN();
xpn.Parse("DOE^JOHN".AsSpan(), HL7Delimiters.Default);

if (xpn.Validate(out var errors))
{
    Console.WriteLine("Valid person name");
}
else
{
    foreach (var error in errors)
    {
        Console.WriteLine($"Error: {error}");
    }
}
```

### Date Ranges (DR)

```csharp
var rangeString = "20230101^20231231";
var dr = new DR();
dr.Parse(rangeString.AsSpan(), HL7Delimiters.Default);

Console.WriteLine($"Start: {dr.StartDate.ToDateTime()}");
Console.WriteLine($"End: {dr.EndDate.ToDateTime()}");

// Validation includes semantic checks
if (!dr.Validate(out var errors))
{
    // Will fail if end date is before start date
    Console.WriteLine(string.Join(", ", errors));
}
```

## Integration with Segments

Composite types are used throughout HL7 segments:

```csharp
using KeryxPars.HL7.Segments;
using KeryxPars.HL7.DataTypes.Composite;

// In the future, segments will use typed properties:
var pid = new PID();
// Future API (Phase 3):
// XPN[] patientNames = pid.PatientName;
// XAD[] addresses = pid.PatientAddress;
// XTN[] phones = pid.PhoneNumberHome;
```

## Performance Characteristics

### Zero-Allocation Parsing
- Uses `ReadOnlySpan<char>` for input
- Custom `ComponentEnumerator` and `SubcomponentEnumerator` (ref structs)
- No heap allocations during parsing

### Stack Allocation
- All types are `readonly struct`
- Stored on the stack, not the heap
- Passed by value or by reference

### Efficient Serialization
- Uses `StringBuilder` with pre-sizing
- `TrimEnd()` to remove trailing separators
- Minimizes string allocations

## Architecture Details

### Component Hierarchy

```
Field
??? Component (separated by ^)
    ??? Subcomponent (separated by &)
```

Example: `123 MAIN ST&APT 5B^OTHER^^CITY^ST^12345`
- Field: Entire address
- Components: Street, Other, Empty, City, State, Zip
- Subcomponents: In street address "123 MAIN ST" & "APT 5B"

### Parsing Flow

1. **Field level**: Segment parser splits fields by `|`
2. **Component level**: `ComponentEnumerator` splits by `^`
3. **Subcomponent level**: `SubcomponentEnumerator` splits by `&`

### Type Safety

Each component is strongly typed:

```csharp
public readonly struct XPN : ICompositeDataType
{
    private readonly FN _familyName;     // Composite (has subcomponents)
    private readonly ST _givenName;      // Primitive
    private readonly ST _secondNames;    // Primitive
    private readonly IS _degree;         // Primitive (coded value)
    // ...
}
```

## Testing

Unit tests verify:
- Parsing HL7 strings correctly
- Serializing back to HL7 format
- Round-trip integrity (parse ? serialize ? parse)
- Validation rules
- Helper methods produce correct output
- Edge cases (empty components, missing components)

## Contributing

When adding new composite types:

1. Follow the existing pattern (see `CE.cs` or `XPN.cs` as examples)
2. Implement all `ICompositeDataType` members
3. Add XML documentation
4. Include helper methods for formatted output
5. Implement semantic validation
6. Add implicit string conversions
7. Write unit tests

## References

- [HL7 Version 2.5 Specification](http://www.hl7.org)
- [Data Types Implementation Plan](../../../Docs/HL7-2.5-DataTypes-Implementation-Plan.md)
- [Phase 2 Progress](../../../Docs/PHASE2-PROGRESS.md)
