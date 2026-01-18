# KeryxPars

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](LICENSE)

> **Keryx** (κῆρυξ) - Greek for "herald" or "messenger" | **Pars** - Latin for "part"

A high-performance, modern .NET HL7 v2.x/FHIR parser built for healthcare interoperability. KeryxPars delivers enterprise-grade speed and memory efficiency while maintaining the flexibility and ease-of-use inspired by System.Text.Json.

## 🎯 Why KeryxPars?

Healthcare software shouldn't be slow and proprietary. KeryxPars was created to provide **open, performant, and configurable tools** that enable innovative healthcare integrations.

Current healthcare interoperability tools are often:
- ❌ Slow and inefficient
- ❌ Proprietary and expensive
- ❌ Difficult to customize
- ❌ Built on outdated technology

KeryxPars changes this by offering:
- 🚀 **Modern .NET**: Built with .NET 8, leveraging `Span<char>` and zero-allocation parsing
- 🔧 **Extensibility**: Fully customizable with generic segment converters
- 📦 **Easy to Use**: Clean API inspired by System.Text.Json
- 🏥 **Healthcare First**: Purpose-built for real-world clinical integrations

## ✨ Key Features

### High Performance
- **Zero-allocation parsing** using `Span<char>` and `ref struct`
- **No reflection** in hot paths - direct property access for maximum speed
- **Lazy initialization** - only allocate what you use
- **Pooled buffers** with `ArrayPool<char>` for serialization
- **Frozen dictionaries** for O(1) segment lookup

### Developer Experience
- **Type-safe** segment definitions
- **Generic converters** - write once, use everywhere
- **Fluent API** for configuration
- **Result<T, E>** pattern for predictable error handling
- **Extensive documentation** and examples

### Extensibility
- **Custom segment support** via `ISegment` interface
- **Pluggable converters** implementing `ISegmentConverter`
- **Configurable order grouping** for medication orders, lab orders, imaging orders
- **Error handling strategies** (FailFast, CollectAndContinue, Silent)
- **Generic segment converter** eliminates boilerplate code

### Enterprise Ready
- **Production-tested** architecture
- **Thread-safe** parsing and serialization
- **Apache 2.0 License** for commercial use

## 🚀 Quick Start

### Installation

```bash
# Using .NET CLI
dotnet add package KeryxPars.HL7

# Using Package Manager
Install-Package KeryxPars.HL7
```

### Parse an HL7 Message

```csharp
using KeryxPars.HL7.Serialization;

var hl7Text = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||";

var result = HL7Serializer.Deserialize(hl7Text);

if (result.IsSuccess)
{
    var message = result.Value;
    Console.WriteLine($"Patient: {message.Pid?.PatientName}");
    Console.WriteLine($"MRN: {message.Pid?.PatientIdentifierList}");
    Console.WriteLine($"DOB: {message.Pid?.DateTimeofBirth}");
}
else
{
    foreach (var error in result.Error!)
    {
        Console.WriteLine($"Error: {error.Message}");
    }
}
```

### Create and Serialize a Message

```csharp
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Segments;
using KeryxPars.HL7.Serialization;

var message = new HL7Message
{
    Msh = new MSH
    {
        SendingApplication = "MY_APP",
        SendingFacility = "MY_FACILITY",
        ReceivingApplication = "TARGET_APP",
        ReceivingFacility = "TARGET_FACILITY",
        MessageType = "ADT^A01",
        MessageControlID = "MSG12345",
        ProcessingID = "P",
        VersionID = "2.5"
    },
    Pid = new PID
    {
        PatientIdentifierList = "123456",
        PatientName = "DOE^JOHN^A",
        DateTimeofBirth = "19800101",
        AdministrativeSex = "M"
    }
};

var result = HL7Serializer.Serialize(message);
if (result.IsSuccess)
{
    Console.WriteLine(result.Value);
}
```

### Working with Medication Orders

```csharp
var orderMessage = @"MSH|^~\&|PHARMACY|HOSPITAL|RX_SYSTEM|HOSPITAL|20230101120000||RDE^O11|ORD001|P|2.5||
PID|1||987654||SMITH^JANE^M||19900215|F|||
ORC|NW|ORD123456|FIL789012||||^^^20230101||
RXE|^^^20230101^20230201|00378-1805-10^Metformin 500mg^NDC|500||MG||||||10|||
RXR|PO^Oral^HL70162|||";

var result = HL7Serializer.Deserialize(orderMessage, SerializerOptions.ForMedicationOrders());

if (result.IsSuccess)
{
    var message = result.Value;
    foreach (var order in message.Orders)
    {
        Console.WriteLine($"Order Control: {order.Orc?.OrderControl}");
        Console.WriteLine($"Medication: {order.Rxe?.GiveCode}");
        Console.WriteLine($"Route: {order.RXR.FirstOrDefault()?.Route}");
    }
}
```

## 🏗️ Architecture

### Project Structure

```
KeryxPars/
├── KeryxPars.Core/              # Core utilities and Result<T,E> pattern
├── KeryxPars.HL7/               # Main HL7 parser library
│   ├── Contracts/               # Interfaces (ISegment, ISegmentConverter)
│   ├── Definitions/             # Core types (HL7Message, OrderGroup)
│   ├── Segments/                # HL7 segment definitions (MSH, PID, etc.)
│   ├── Serialization/           # Parser engine and converters
│   │   ├── Converters/          # GenericSegmentConverter<T>
│   │   └── Configuration/       # SerializerOptions, OrderGrouping
│   └── Extensions/              # Helper methods
├── KeryxPars.Benchmarks/        # Performance benchmarks
└── KeryxPars.Sandbox/           # Sample applications
```

### Core Components

#### 1. Zero-Allocation Parser
```csharp
// Uses Span<char> for zero-copy parsing
public ref struct SegmentReader
{
    private ReadOnlySpan<char> _segment;
    
    public bool TryReadField(char delimiter, out ReadOnlySpan<char> field)
    {
        // Direct span slicing - no allocations
    }
}
```

#### 2. Generic Segment Converter
```csharp
public class GenericSegmentConverter<TSegment> : ISegmentConverter 
    where TSegment : ISegment, new()
{
    public Result<ISegment, HL7Error> Read(ref SegmentReader reader, ...)
    {
        var segment = new TSegment();
        // Parse fields using SetValue()
        return segment;
    }
}
```

#### 3. Result Pattern
```csharp
public readonly struct Result<TValue, TError>
{
    public bool IsSuccess { get; }
    public TValue? Value { get; }
    public TError? Error { get; }
}
```

## 📖 Documentation

### Supported Segments

**Production-ready segments** (primarily pharmacy-focused):

| Segment | Description | Repeatable | Fields |
|---------|-------------|------------|--------|
| **MSH** | Message Header | No | 13 |
| **MSA** | Message Acknowledgement | No | 9 |
| **ERR** | Error Information | Yes | 13 |
| **EVN** | Event Type | No | 8 |
| **PID** | Patient Identification | No | 41 |
| **PD1** | Additional Demographics | No | 23 |
| **NK1** | Next of Kin | Yes | 6 |
| **PV1** | Patient Visit | No | 55 |
| **PV2** | Patient Visit Additional | No | 51 |
| **AL1** | Allergy Information | Yes | 7 |
| **DG1** | Diagnosis Information | Yes | 27 |
| **IN1** | Insurance Information | Yes | 33 |
| **ORC** | Common Order | Yes | 34 |
| **RXE** | Pharmacy Encoded Order | No | 46 |
| **RXO** | Pharmacy Prescription Order | No | 37 |
| **RXR** | Pharmacy Route | Yes | 7 |
| **RXC** | Pharmacy Component | Yes | 10 |
| **TQ1** | Timing Quantity | Yes | 15 |
| **TQ2** | Timing/Quantity Relationship | Yes | 11 |
| **OBX** | Observation/Result | Yes | 27 |

> **Note**: Field support is comprehensive for pharmacy communications. Additional fields and segments are being added continuously. See the [Roadmap](#🗺️-roadmap) for expansion plans.

### Supported Message Types

- **ADT** (Admission/Discharge/Transfer): A01, A03, A04, A05, A08, A10, A14
- **RDE** (Pharmacy/Treatment Encoded Order): O11
- **VXU** (Immunization): V04
- **Custom message types** supported through extensible architecture

### Adding Custom Segments

```csharp
using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

public class ZZZ : ISegment
{
    public string SegmentId => nameof(ZZZ);
    
    public string CustomField1 { get; set; } = string.Empty;
    public string CustomField2 { get; set; } = string.Empty;
    
    public void SetValue(string value, int fieldIndex)
    {
        switch (fieldIndex)
        {
            case 1: CustomField1 = value; break;
            case 2: CustomField2 = value; break;
        }
    }
    
    public string[] GetValues()
    {
        return [SegmentId, CustomField1, CustomField2];
    }
    
    public string? GetField(int index) => index switch
    {
        0 => SegmentId,
        1 => CustomField1,
        2 => CustomField2,
        _ => null
    };
}

// Register the converter
var registry = new DefaultSegmentRegistry();
registry.Register(new GenericSegmentConverter<ZZZ>());

var options = new SerializerOptions { SegmentRegistry = registry };
var result = HL7Serializer.Deserialize(message, options);
```

### Configuration Options

```csharp
// Default configuration
var options = SerializerOptions.Default;

// Medication order grouping
var medOptions = SerializerOptions.ForMedicationOrders();

// Lab order grouping
var labOptions = SerializerOptions.ForLabOrders();

// Imaging order grouping
var imagingOptions = SerializerOptions.ForImagingOrders();

// Custom configuration
var customOptions = new SerializerOptions
{
    ErrorHandling = ErrorHandlingStrategy.FailFast,
    IgnoreUnknownSegments = false,
    InitialBufferSize = 8192,
    OrderGrouping = OrderGroupingConfiguration.Medication
};
```

### Error Handling

```csharp
var result = HL7Serializer.Deserialize(message);

if (result.IsSuccess)
{
    var hl7Message = result.Value;
    // Process message
}
else
{
    // Handle errors
    foreach (var error in result.Error!)
    {
        Console.WriteLine($"[{error.Severity}] {error.Code}: {error.Message}");
    }
    
    // Or generate NACK
    var nack = result.AsNack("SendingFacility", "SendingApplication", "2.5");
    Console.WriteLine(nack);
}
```

## 🗺️ Roadmap

### Short Term ✅
- ✅ High-performance parsing engine
- ✅ Generic segment converters (no boilerplate!)
- ✅ Lazy initialization for memory efficiency
- ✅ Comprehensive benchmarks vs. popular libraries
- ✅ Zero-allocation parsing with Span<char>
- 🔄 Expand segment field support (in progress)
- 🔄 Additional HL7 v2.x message types
- 🔄 Comprehensive unit tests

### Medium Term 🎯
- 🔲 **Source Generators**: Auto-generate converters between `HL7Message` and custom objects (HL7 AutoMapper)
- 🔲 **All HL7 v2.x Versions**: Complete support for HL7 v2.1 through v2.9
- 🔲 Validation framework for segment rules and message structure
- 🔲 Enhanced error messages and diagnostics
- 🔲 Performance profiling tools and optimization guides
- 🔲 NuGet package publication

### Long Term 🚀
- 🔲 **FHIR Support**: HL7 FHIR parser and converters
- 🔲 **HL7 v3** support
- 🔲 **Bidirectional Conversion**: FHIR ↔ HL7 v2.x transformation


## 🤝 Contributing

**Contributions are welcome!** KeryxPars aims to democratize healthcare interoperability tools and make them accessible to everyone.

### How to Contribute

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/amazing-feature`
3. **Make your changes**
4. **Run benchmarks**: Ensure no performance regressions
5. **Commit your changes**: `git commit -m 'Add amazing feature'`
6. **Push to branch**: `git push origin feature/amazing-feature`
7. **Open a Pull Request**

### Development Setup

```bash
# Clone the repository
git clone https://github.com/yourusername/KeryxPars.git
cd KeryxPars

# Build the solution
dotnet build

# Run tests
dotnet test

# Run benchmarks
cd KeryxPars.Benchmarks
dotnet run -c Release
```

### Areas for Contribution

- 📋 **Segment Definitions**: Expand field support in existing segments
- 🆕 **New Segments**: Add support for additional HL7 segments (OBR, NTE, etc.)
- 🧪 **Tests**: Increase test coverage and add edge cases
- 📖 **Documentation**: Improve docs, add examples, write tutorials
- 🐛 **Bug Fixes**: Fix issues and handle edge cases
- ⚡ **Performance**: Optimize hot paths and reduce allocations
- 🔌 **Integrations**: Add support for new message types and HL7 versions
- 🎨 **Tooling**: Build developer tools, analyzers, or IDE extensions

### Contribution Guidelines

- **Code Style**: Follow existing patterns and conventions
- **Performance**: Don't introduce allocations in hot paths
- **Testing**: Add tests for new features
- **Documentation**: Update README and XML docs
- **Benchmarks**: Verify no performance regressions

## 📋 Requirements

- **.NET 8.0** or higher
- **Visual Studio 2022** or **JetBrains Rider** (recommended for development)
- **VS Code** with C# Dev Kit also supported

## 📜 License

This project is licensed under the **Apache License 2.0** - see the [LICENSE](LICENSE) file for details.

The Apache 2.0 license provides:
- ✅ **Commercial use** - Use in proprietary software
- ✅ **Modification** - Adapt to your needs
- ✅ **Distribution** - Share with others
- ✅ **Patent protection** - Explicit patent grant
- ✅ **Private use** - Use internally without disclosure

This license was chosen specifically for healthcare IT to provide:
- Patent protection (important in healthcare)
- Enterprise-friendly terms
- Clear legal framework for commercial adoption
- Protection for contributors

## 🙏 Acknowledgments

- **Inspired by** the performance and API design of **System.Text.Json**
- **Built on** modern .NET features: `Span<T>`, `ref struct`, collection expressions, and more
- **Benchmarking** powered by **BenchmarkDotNet**
- **Validated against** **HL7-dotnetcore** and **NHapi** for compatibility
- **Community-driven** with the goal of democratizing healthcare interoperability

## 📞 Support & Community

- 🐛 **Report Issues**: [GitHub Issues](https://github.com/yourusername/KeryxPars/issues)
- 💬 **Discussions**: [GitHub Discussions](https://github.com/yourusername/KeryxPars/discussions)
- 📖 **Documentation**: [Wiki](https://github.com/yourusername/KeryxPars/wiki)
- 📧 **Contact**: For security issues or private inquiries

## 🌟 Star History

If you find KeryxPars useful, please consider giving it a star ⭐ on GitHub!

---

<div align="center">

**KeryxPars** - Bringing speed, simplicity, and accessibility to healthcare interoperability 🏥⚡

*Named after the Greek κῆρυξ (keryx) meaning "herald" or "messenger", and the Latin "pars" meaning "part" - a fitting tribute to the art of parsing interoperability messages.*

Made with ❤️ for the healthcare community

</div>
