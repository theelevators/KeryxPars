# HL7 2.5 Data Types

This directory contains the implementation of HL7 2.5 data types according to the HL7 Version 2.5 specification.

## Structure

```
DataTypes/
??? Contracts/           # Base interfaces for all data types
?   ??? IHL7DataType.cs
?   ??? IPrimitiveDataType.cs
?   ??? ICompositeDataType.cs
??? Primitive/           # Simple atomic data types
?   ??? ST.cs           # String
?   ??? TX.cs           # Text
?   ??? FT.cs           # Formatted Text
?   ??? ID.cs           # Coded Value (HL7 tables)
?   ??? IS.cs           # Coded Value (User tables)
?   ??? NM.cs           # Numeric
?   ??? SI.cs           # Sequence ID
?   ??? DT.cs           # Date
?   ??? TM.cs           # Time
?   ??? DTM.cs          # DateTime
??? Composite/           # Complex multi-component data types (Phase 2+)
    ??? (To be implemented)
```

## Primitive Data Types (Phase 1 - ? Complete)

### String Types
- **ST** - String (max 199 chars)
- **TX** - Text (unlimited length)
- **FT** - Formatted Text

### Coded Value Types
- **ID** - Coded Value from HL7-defined tables (max 199 chars)
- **IS** - Coded Value from user-defined tables (max 20 chars)

### Numeric Types
- **NM** - Numeric value (max 16 chars recommended)
- **SI** - Sequence ID (non-negative integer)

### Date/Time Types
- **DT** - Date (YYYY[MM[DD]])
- **TM** - Time (HH[MM[SS[.SSSS]]][+/-ZZZZ])
- **DTM** - DateTime (YYYY[MM[DD[HH[MM[SS[.SSSS]]]]]]][+/-ZZZZ])

## Usage Examples

### Basic Usage

```csharp
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;

// String data
ST patientName = "JOHN DOE";
string value = patientName; // Implicit conversion

// Date data
DT birthDate = new DateTime(1980, 5, 15);
DateTime? dt = birthDate.ToDateTime();

// Numeric data
NM temperature = 98.6;
decimal? temp = temperature.ToDecimal();

// Parsing from HL7
var date = new DT();
date.Parse("20230115".AsSpan(), HL7Delimiters.Default);

// Validation
if (date.Validate(out var errors))
{
    Console.WriteLine("Valid date");
}
```

### Parsing HL7 Fields

```csharp
// Parse a field from an HL7 message
var delimiters = HL7Delimiters.Default;
var dateField = "20230115";

var dt = new DT();
dt.Parse(dateField.AsSpan(), delimiters);

if (dt.Validate(out var errors))
{
    var dateTime = dt.ToDateTime();
    // Use the parsed DateTime
}
```

### Building HL7 Output

```csharp
var delimiters = HL7Delimiters.Default;

DT admitDate = DateTime.Today;
NM age = 45;
ID sex = "M";

var hl7Date = admitDate.ToHL7String(delimiters);
var hl7Age = age.ToHL7String(delimiters);
var hl7Sex = sex.ToHL7String(delimiters);

// Build segment
var segment = $"PID|1||12345|||{hl7Date}|{hl7Sex}";
```

## Design Principles

### 1. Type Safety
All data types are strongly typed, preventing common errors:
```csharp
DT date = "20230115"; // ? Correct
DT date = "2023-01-15"; // ? Will fail validation
```

### 2. Implicit Conversions
Seamless conversion between HL7 types and .NET native types:
```csharp
DT date = new DateTime(2023, 1, 15); // Implicit from DateTime
DateTime? dt = date.ToDateTime(); // Convert back
```

### 3. Zero-Allocation Parsing
Uses `Span<char>` for parsing without string allocations:
```csharp
dt.Parse(value.AsSpan(), delimiters); // No heap allocations
```

### 4. Validation
Built-in validation according to HL7 2.5 rules:
```csharp
if (dataType.Validate(out var errors))
{
    // Valid data
}
else
{
    // errors contains list of validation failures
}
```

### 5. Immutability
Primitive types are implemented as `readonly struct` for safety and performance.

## Performance Characteristics

- **Memory**: Value types (structs) - no heap allocations
- **Parsing**: Zero-allocation using `ReadOnlySpan<char>`
- **Validation**: Efficient with minimal string operations
- **Conversion**: Implicit operators avoid boxing/unboxing

## Testing

All primitive data types have comprehensive unit tests in:
```
Tests/KeryxPars.HL7.Tests/DataTypes/Primitive/
```

Run tests:
```bash
dotnet test --filter "FullyQualifiedName~DataTypes.Primitive"
```

## Composite Data Types (Phase 2 - In Progress)

The following composite types are planned for implementation:

### High Priority
- **XPN** - Extended Person Name
- **XAD** - Extended Address  
- **XTN** - Extended Telecom Number
- **CX** - Extended Composite ID
- **CE** - Coded Element
- **CWE** - Coded with Exceptions
- **TS** - Timestamp
- **XCN** - Extended Composite ID Number and Name
- **PL** - Person Location
- **EI** - Entity Identifier
- **CQ** - Composite Quantity with Units

### Medium Priority
- **HD** - Hierarchic Designator
- **CP** - Composite Price
- **TQ** - Timing Quantity
- **SN** - Structured Numeric
- **XON** - Extended Composite Name and ID for Organizations
- And more...

## References

- [HL7 Version 2.5 Specification](http://www.hl7.org/)
- [Implementation Plan](../../Docs/HL7-2.5-DataTypes-Implementation-Plan.md)
- [Phase 1 Summary](../../Docs/Phase1-Implementation-Summary.md)
- [Usage Examples](../Examples/DataTypeExamples.cs)

## Contributing

When adding new data types:

1. Implement the appropriate interface (`IPrimitiveDataType` or `ICompositeDataType`)
2. Follow existing patterns for consistency
3. Add comprehensive unit tests
4. Update this README with the new type
5. Add examples to `DataTypeExamples.cs`

## Status

- ? **Phase 1 Complete**: All primitive data types implemented and tested
- ?? **Phase 2 In Progress**: Composite data types implementation
- ?? **Phase 3 Planned**: Medium and low priority types
- ?? **Phase 4 Planned**: Segment refactoring to use typed properties
